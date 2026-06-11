using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance { get; private set; }
    private void Awake() => Instance = this;
    public void PlaySFX(string n) {}
}
