using UnityEngine;

public class EnemyPatrolState : IState {
    private readonly EnemyController _c; private float _t, _d; public EnemyPatrolState(EnemyController c) => _c = c;
    public void Enter() { _t = 2f; _c.Anim.Play(EnemyController.HashMove); _d = Random.value > 0.5f ? 1f : -1f; _c.transform.localScale = new Vector3(_d, 1f, 1f); }
    public void Update() { if (_c.CanSeePlayer()) { _c.StateMachine.ChangeState(_c.ChaseState); return; } _t -= Time.deltaTime; if (_t <= 0) _c.StateMachine.ChangeState(_c.IdleState); }
    public void FixedUpdate() => _c.Rb.linearVelocity = new Vector2(_d * _c.moveSpeed * 0.6f, _c.Rb.linearVelocity.y);
    public void Exit() {}
}
