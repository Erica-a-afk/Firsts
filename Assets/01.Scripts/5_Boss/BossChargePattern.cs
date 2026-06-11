using UnityEngine;

public class BossChargePattern : MonoBehaviour {
    public void Charge(Rigidbody2D rb, float f) => rb.linearVelocity = new Vector2(transform.localScale.x * f, rb.linearVelocity.y);
}
