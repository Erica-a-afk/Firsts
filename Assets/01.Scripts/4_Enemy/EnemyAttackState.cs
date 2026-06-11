using UnityEngine;

public class EnemyAttackState : IState {
    private readonly EnemyController _c; private readonly EnemyAttack _a; public EnemyAttackState(EnemyController c) { _c = c; _a = c.GetComponent<EnemyAttack>(); }
    public void Enter() { _c.Rb.linearVelocity = Vector2.zero; _a?.TriggerAttack(); }
    public void Update() { if (_a != null && !_a.IsAttacking) _c.StateMachine.ChangeState(_c.IdleState); }
    public void FixedUpdate() {} public void Exit() {}
}
