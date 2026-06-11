using UnityEngine;

public class EnemyLootDrop : MonoBehaviour {
    [SerializeField] private GameObject prefab;
    public void Drop() { if (prefab) Instantiate(prefab, transform.position, Quaternion.identity); }
}
