using UnityEngine;

public interface IProjectilePool {
    GameObject SpawnProjectile(Vector3 pos, Quaternion rot);
    void DespawnProjectile(GameObject proj);
}
