using UnityEngine;

public class BossAreaDenial : MonoBehaviour {
    [SerializeField] private LayerMask layer;
    private void Update() { Collider2D c = Physics2D.OverlapBox(transform.position, transform.localScale, 0f, layer); c?.GetComponent<IDamageable>()?.TakeDamage(2f * Time.deltaTime); }
}
