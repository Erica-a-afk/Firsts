using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyAttackDetector))]
public class EnemyAttack : MonoBehaviour {
    [SerializeField] private DamageAreaSO attackDataSO; 
    [SerializeField] private LayerMask playerLayer;
    private EnemyAttackDetector _det; private int _idx; public bool IsAttacking { get; private set; }
    
    private void Awake() => _det = GetComponent<EnemyAttackDetector>();
    public void TriggerAttack() { if (!IsAttacking && attackDataSO) StartCoroutine(Seq()); }
    private IEnumerator Seq() {
        IsAttacking = true; _det.Clear(); GetComponent<EnemyController>().Anim.Play(EnemyController.HashAttack);
        yield return new WaitForSeconds(attackDataSO.attackExecuteTime);
        if (attackDataSO.combos.Length > 0) {
            AttackComboData cm = attackDataSO.combos[_idx % attackDataSO.combos.Length];
            Vector2 c = (Vector2)transform.position + new Vector2(cm.offsetX * (transform.localScale.x > 0 ? 1f : -1f), cm.offsetY);
            float finalDmg = attackDataSO.baseDamage * (GetComponent<EnemyController>() != null ? GetComponent<EnemyController>().damageModifier : 1f);
            foreach (var t in _det.Detect(c, new Vector2(cm.attackRangeX, cm.attackRangeY), playerLayer)) t.TakeDamage(finalDmg);
            _idx++;
        }
        yield return new WaitForSeconds(0.5f); IsAttacking = false;
    }
}
