using UnityEngine;

public class SaveLoadSystem : MonoBehaviour {
    public void Save() => PlayerPrefs.SetFloat("HP", 100f);
    public void Load() {}
}
