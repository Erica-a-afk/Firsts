using UnityEngine;

[CreateAssetMenu(menuName = "SO/DamageArea")]
public class DamageAreaSO : ScriptableObject {
    public float baseDamage = 10f;
    public float attackExecuteTime = 0.2f;
    public AttackComboData[] combos;
}
