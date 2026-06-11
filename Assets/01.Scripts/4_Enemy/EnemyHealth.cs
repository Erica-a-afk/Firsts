using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable {
        public float CurrentHealth { get; set; } 
        public float MaxHealth { get; set; } = 40f;
        private void Start() => CurrentHealth = MaxHealth;
    
        public void TakeDamage(float dmg) {
                if (GetComponent<Enemy_Warden>() != null) return;
                CurrentHealth -= dmg; var c = GetComponent<EnemyController>();
                if (CurrentHealth <= 0f) c.StateMachine.ChangeState(c.DieState); 
                else c.StateMachine.ChangeState(c.HitState);
        }
    
        public void Die() {
                int gold = GetComponent<EnemyController>() != null ? GetComponent<EnemyController>().baseGoldReward : 20;
                int finalGold = StageProgressManager.Instance != null ? StageProgressManager.Instance.GetEnemyGoldReward(gold) : gold;
        
                // 피드백 반영: FindFirstObjectByType 대신 최신 규격인 FindAnyObjectByType 사용!
                Object.FindAnyObjectByType<PlayerStatsModule>()?.AddGold(finalGold);
        
                if (TryGetComponent(out EnemyLootDrop ld)) ld.Drop();
                gameObject.SetActive(false);
        }
}