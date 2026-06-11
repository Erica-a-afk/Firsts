using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour {
    public static CameraManager Instance { get; private set; }
    [SerializeField] private CinemachineCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin _perlin;
    private Coroutine _cor;
    private void Awake() {
        if (Instance == null) Instance = this; else Destroy(gameObject);
        if (virtualCamera) _perlin = virtualCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
    }
    public void ShakeCamera(float intensity, float duration) {
        if (_perlin != null) { if (_cor != null) StopCoroutine(_cor); _cor = StartCoroutine(Shake(intensity, duration)); }
    }
    private IEnumerator Shake(float i, float d) { _perlin.AmplitudeGain = i; yield return new WaitForSeconds(d); _perlin.AmplitudeGain = 0f; }
    private void OnDisable() { if (_cor != null) StopCoroutine(_cor); if (_perlin != null) _perlin.AmplitudeGain = 0f; }
}
