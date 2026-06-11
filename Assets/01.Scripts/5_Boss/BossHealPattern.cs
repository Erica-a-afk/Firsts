using UnityEngine;

public class BossHealPattern : MonoBehaviour {
    public void Heal(IDamageable b, float v) => b.CurrentHealth = Mathf.Min(b.MaxHealth, b.CurrentHealth + v);
}
