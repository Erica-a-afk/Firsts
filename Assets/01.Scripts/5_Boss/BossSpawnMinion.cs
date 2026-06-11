using UnityEngine;

public class BossSpawnMinion : MonoBehaviour {
    public GameObject minion;
    public void Spawn(Vector3 p) { if (minion) Instantiate(minion, p, Quaternion.identity); }
}
