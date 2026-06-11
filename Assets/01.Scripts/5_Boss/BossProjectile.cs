using UnityEngine;

public class BossProjectile : MonoBehaviour {
    public float speed = 8f, damage = 12f;
    private void Start() => Destroy(gameObject, 4f);
    private void Update() => transform.Translate(Vector2.right * speed * Time.deltaTime);
    private void OnTriggerEnter2D(Collider2D col) { if (col.CompareTag("Player")) { col.GetComponent<IDamageable>()?.TakeDamage(damage); Destroy(gameObject); } }
}
