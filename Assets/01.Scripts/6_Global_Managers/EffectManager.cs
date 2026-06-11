using UnityEngine;

public class EffectManager : MonoBehaviour {
    public static EffectManager Instance { get; private set; }
    private void Awake() => Instance = this;
    public void Spawn(GameObject p, Vector3 pos) => Instantiate(p, pos, Quaternion.identity);
}
