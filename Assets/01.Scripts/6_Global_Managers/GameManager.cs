using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    public System.Action OnStateChanged;
    private void Awake() { if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); SceneManager.sceneLoaded += OnLoad; } else Destroy(gameObject); }
    private void OnLoad(Scene s, LoadSceneMode m) => OnStateChanged = null;
    private void OnDestroy() => SceneManager.sceneLoaded -= OnLoad;
}
