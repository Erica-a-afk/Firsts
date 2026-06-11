using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

[InitializeOnLoad]
public static class UnityMcpBridge
{
    private static HttpListener _listener;
    private static bool _isRunning;
    private const string Port = "54321";

    static UnityMcpBridge()
    {
        StopServer();
        StartServer();
    }

    public static void StartServer()
    {
        if (_isRunning) return;
        try
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://localhost:{Port}/mcp/");
            _listener.Start();
            _isRunning = true;
            Debug.Log($"🚀 [Unity MCP Bridge] 터미널 전용 제어 서버 가동 (Port: {Port})");
            Task.Run(() => ListenLoop());
        }
        catch (Exception e)
        {
            Debug.LogError($"❌ [Unity MCP] 서버 시작 실패: {e.Message}");
        }
    }

    private static void StopServer()
    {
        try { if (_listener != null) { _listener.Stop(); _listener.Close(); } } catch { }
        _isRunning = false;
    }

    private static async Task ListenLoop()
    {
        while (_isRunning && _listener != null)
        {
            try { var context = await _listener.GetContextAsync(); ProcessRequest(context); } catch { }
        }
    }

    private static void ProcessRequest(HttpListenerContext context)
    {
        string responseString = "Success";
        try
        {
            using (var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
            {
                string jsonBody = reader.ReadToEnd();
                EditorApplication.delayCall += () => { ExecuteEditorCommand(jsonBody); };
            }
        }
        catch (Exception e) { responseString = $"Error: {e.Message}"; }

        try
        {
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            context.Response.ContentType = "text/plain";
            context.Response.ContentLength64 = buffer.Length;
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            context.Response.OutputStream.Close();
        }
        catch { }
    }

    private static void ExecuteEditorCommand(string jsonBody)
    {
        try
        {
            string action = ExtractJsonValue(jsonBody, "action");
            string dataSection = ExtractJsonSection(jsonBody, "data");

            switch (action)
            {
                case "create_unity_object": HandleCreateObject(dataSection); break;
                case "add_unity_component": HandleAddComponent(dataSection); break;
                case "set_unity_component_field": HandleSetField(dataSection); break;
            }
        }
        catch (Exception e) { Debug.LogError($"❌ [Unity MCP] 명령 반영 오류: {e.Message}"); }
    }

    private static void HandleCreateObject(string data)
    {
        string name = ExtractJsonValue(data, "name");
        string tag = ExtractJsonValue(data, "tag");
        string parentName = ExtractJsonValue(data, "parentName");

        if (string.IsNullOrEmpty(name)) return;

        GameObject go = new GameObject(name);
        Undo.RegisterCreatedObjectUndo(go, $"Create {name}");

        if (!string.IsNullOrEmpty(parentName))
        {
            GameObject parent = GameObject.Find(parentName);
            if (parent != null) go.transform.SetParent(parent.transform);
        }

        if (!string.IsNullOrEmpty(tag) && !string.IsNullOrWhiteSpace(tag)) go.tag = tag;
        Debug.Log($"🎯 [Unity MCP] 오브젝트 생성 완료: {name}");
    }

    // ★ [핵심 고도화] 유니티 내부의 모든 어셈블리/네임스페이스 완전 추적기
    private static void HandleAddComponent(string data)
    {
        string objectName = ExtractJsonValue(data, "objectName");
        string componentName = ExtractJsonValue(data, "componentName");

        if (string.IsNullOrEmpty(objectName) || string.IsNullOrEmpty(componentName)) return;

        GameObject go = GameObject.Find(objectName);
        if (go == null) return;

        Type type = null;

        // 유니티 시스템에 로드된 모든 DLL(어셈블리)을 이 잡듯 뒤져서 클래스 이름을 매핑합니다.
        // 이렇게 하면 UnityEngine, UnityEngine.Audio, UnityEngine.Animation 등 어디에 박혀있든 다 찾아냅니다.
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            // 1. 네임스페이스가 생략된 경우를 위해 풀네임 뒤쪽 일치 확인
            foreach (var t in assembly.GetTypes())
            {
                if (t.Name == componentName && typeof(Component).IsAssignableFrom(t))
                {
                    type = t;
                    break;
                }
            }
            if (type != null) break;
        }

        // 찾아낸 타입이 컴포넌트 계열이면 인스펙터에 강제 부착
        if (type != null)
        {
            Undo.AddComponent(go, type);
            Debug.Log($"🎯 [Unity MCP] 컴포넌트 부착 성공: {objectName} -> {componentName}");
        }
        else
        {
            Debug.LogWarning($"⚠️ [Unity MCP] '{componentName}' 컴포넌트 타입을 유니티 전체 시스템에서 찾을 수 없습니다.");
        }
    }

    private static void HandleSetField(string data)
    {
        string objectName = ExtractJsonValue(data, "objectName");
        string componentName = ExtractJsonValue(data, "componentName");
        string fieldName = ExtractJsonValue(data, "fieldName");
        string valueStr = ExtractJsonValue(data, "value");

        GameObject go = GameObject.Find(objectName);
        if (go == null) return;

        Component comp = go.GetComponent(componentName);
        if (comp == null) return;

        Undo.RecordObject(comp, "Modify Field Value");

        var type = comp.GetType();
        var prop = type.GetProperty(fieldName, BindingFlags.Public | BindingFlags.Instance);
        var field = type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        Type targetType = prop != null ? prop.PropertyType : (field != null ? field.FieldType : null);
        if (targetType == null) return;

        object finalValue = null;

        if (typeof(UnityEngine.Object).IsAssignableFrom(targetType) && valueStr.StartsWith("Assets/"))
        {
            finalValue = AssetDatabase.LoadAssetAtPath(valueStr, targetType);
            if (finalValue == null) return;
        }
        else
        {
            finalValue = Convert.ChangeType(valueStr, targetType);
        }

        if (prop != null && prop.CanWrite) prop.SetValue(comp, finalValue);
        else if (field != null) field.SetValue(comp, finalValue);

        EditorUtility.SetDirty(comp);
    }

    private static string ExtractJsonValue(string json, string key)
    {
        string search = $"\"{key}\"";
        int index = json.IndexOf(search);
        if (index == -1) return "";
        int start = json.IndexOf(":", index) + 1;
        while (start < json.Length && (json[start] == ' ' || json[start] == '"' || json[start] == ':')) start++;
        int end = start;
        while (end < json.Length && json[end] != '"' && json[end] != ',' && json[end] != '}' && json[end] != '\r' && json[end] != '\n') end++;
        return json.Substring(start, end - start).Trim().Replace("\"", "");
    }

    private static string ExtractJsonSection(string json, string key)
    {
        string search = $"\"{key}\"";
        int index = json.IndexOf(search);
        if (index == -1) return json;
        int start = index + search.Length;
        return json.Substring(start).Trim();
    }
}