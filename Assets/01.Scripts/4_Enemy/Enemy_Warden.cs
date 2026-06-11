using UnityEngine;

public class Enemy_Warden : MonoBehaviour, IDamageable {
        public float CurrentHealth { get; set; } = 1f;
        public float MaxHealth { get; set; } = 1f;

        public void TakeDamage(float damage) { CurrentHealth = 0; Die(); }
        public void Die() {
                GetComponentInChildren<Animator>().Play(EnemyController.HashDeath);
                int finalGold = StageProgressManager.Instance != null ? StageProgressManager.Instance.GetEnemyGoldReward(50) : 50;
        
                // 피드백 반영: FindFirstObjectByType 대신 최신 규격인 FindAnyObjectByType 사용!
                Object.FindAnyObjectByType<PlayerStatsModule>()?.AddGold(finalGold);
        
                if (TryGetComponent(out EnemyLootDrop ld)) ld.Drop();
                gameObject.SetActive(false);
        }
}