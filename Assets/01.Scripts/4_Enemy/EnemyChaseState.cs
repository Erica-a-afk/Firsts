using UnityEngine;

public class EnemyChaseState : IState {
    private readonly EnemyController _c; public EnemyChaseState(EnemyController c) => _c = c;
    public void Enter() => _c.Anim.Play(EnemyController.HashMove);
    public void Update() {
        if (!_c.CanSeePlayer()) { _c.StateMachine.ChangeState(_c.IdleState); return; }
        if (_c.CanAttackPlayer()) { _c.StateMachine.ChangeState(_c.AttackState); return; }
        _c.transform.localScale = new Vector3(_c.Target.position.x - _c.transform.position.x > 0 ? 1f : -1f, 1f, 1f);
    }
    public void FixedUpdate() { if (_c.Target) _c.Rb.linearVelocity = new Vector2(Mathf.Sign(_c.Target.position.x - _c.transform.position.x) * _c.moveSpeed, _c.Rb.linearVelocity.y); }
    public void Exit() {}
}
