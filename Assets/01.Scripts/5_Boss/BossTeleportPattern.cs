using UnityEngine;

public class BossTeleportPattern : MonoBehaviour {
    public void Teleport(Vector3 p) => transform.position = p;
}
