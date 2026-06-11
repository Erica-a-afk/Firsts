using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour, IProjectilePool {
    public static ObjectPoolManager Instance { get; private set; }
    [SerializeField] private GameObject prefab;
    private IObjectPool<GameObject> _pool;
    private void Awake() {
        Instance = this;
        _pool = new ObjectPool<GameObject>(
            () => Instantiate(prefab, transform),
            o => o.SetActive(true),
            o => o.SetActive(false),
            Destroy, true, 20, 50);
    }
    public GameObject SpawnProjectile(Vector3 p, Quaternion r) { GameObject o = _pool.Get(); o.transform.SetPositionAndRotation(p, r); return o; }
    public void DespawnProjectile(GameObject o) => _pool.Release(o);
}
