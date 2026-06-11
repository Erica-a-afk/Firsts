using UnityEngine;

public class AncientBossHealth : MonoBehaviour, IDamageable {
    public float CurrentHealth { get; set; } 
    public float MaxHealth { get; set; } = 300f;
    private void Start() => CurrentHealth = MaxHealth;
    public void TakeDamage(float dmg) {
        CurrentHealth -= dmg; var c = GetComponent<AncientBossController>();
        if (CurrentHealth <= 0f) {
            c.Anim.Play(AncientBossController.HashDeath);
            GlobalEventBroadcaster.Trigger();
            gameObject.SetActive(false);
        }
        else if (CurrentHealth / MaxHealth <= c.phase2Threshold && c.StateMachine.CurrentState == c.Phase1State) {
            c.StateMachine.ChangeState(c.GimmickState);
        }
    }
    public void Die() => gameObject.SetActive(false);
}
