using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
        [SerializeField] private DamageAreaSO damageArea; 
        [SerializeField] private LayerMask enemyLayer;
        private int _idx; 
        private bool _rollIdx; 
        private readonly List<IDamageable> _history = new List<IDamageable>();

        private PlayerStatsModule _stats;

        private void Awake() {
                // 부모 본체에 단 하나 붙은 통합 스탯 허브 모듈 연동 캐싱
                _stats = GetComponentInParent<PlayerStatsModule>();
        }

        public DamageAreaSO AttackDataSO => damageArea;

        public void SetComboIndex(int i) { 
                _idx = i; 
                _history.Clear(); 
                ExecuteHit(); 
        }
    
        public void SetRollingAttackBonus(bool s) => _rollIdx = s;

        private void ExecuteHit() {
                if (!damageArea || _idx >= damageArea.combos.Length) return;
                AttackComboData c = damageArea.combos[_idx]; 
                Vector2 sz = new Vector2(c.attackRangeX, c.attackRangeY);
        
                // 부모 본체(transform.parent)의 바라보는 스케일 축에 맞춰 히트박스 전방 반전 연산
                Vector2 center = (Vector2)transform.position + new Vector2(c.offsetX * (transform.parent.localScale.x > 0 ? 1f : -1f), c.offsetY);
        
                // 뚱뚱한 스크립트를 밀어버리고 가져온 통합 스탯 모듈 대미지 수치 적용
                float dmg = (_stats != null ? _stats.FinalDamage : damageArea.baseDamage);
                if (_rollIdx) dmg *= 1.5f;

                foreach (var col in Physics2D.OverlapBoxAll(center, sz, 0f, enemyLayer)) {
                        if (col.TryGetComponent(out IDamageable t) && !_history.Contains(t) && col.gameObject != transform.parent.gameObject) { 
                                _history.Add(t); 
                                t.TakeDamage(dmg); 
                        }
                }
        }
}