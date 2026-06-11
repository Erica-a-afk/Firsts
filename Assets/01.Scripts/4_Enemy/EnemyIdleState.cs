using UnityEngine;

public class EnemyIdleState : IState {
    private readonly EnemyController _c; private float _t; public EnemyIdleState(EnemyController c) => _c = c;
    public void Enter() { _t = 1.5f; _c.Anim.Play(EnemyController.HashIdle); _c.Rb.linearVelocity = Vector2.zero; }
    public void Update() { if (_c.CanSeePlayer()) { _c.StateMachine.ChangeState(_c.ChaseState); return; } _t -= Time.deltaTime; if (_t <= 0) _c.StateMachine.ChangeState(_c.PatrolState); }
    public void FixedUpdate() {} public void Exit() {}
}
