using UnityEngine;

public class BossShockwave : MonoBehaviour {
    private void Start() => Destroy(gameObject, 0.5f);
    private void OnTriggerEnter2D(Collider2D col) { if (col.CompareTag("Player")) col.GetComponent<IDamageable>()?.TakeDamage(18f); }
}
