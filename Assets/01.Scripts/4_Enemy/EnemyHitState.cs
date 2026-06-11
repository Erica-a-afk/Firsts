using UnityEngine;

public class EnemyHitState : IState {
    private readonly EnemyController _c; private float _t; public EnemyHitState(EnemyController c) => _c = c;
    public void Enter() { _t = 0.2f; _c.Anim.Play(EnemyController.HashHit); _c.Rb.linearVelocity = new Vector2(_c.transform.localScale.x * -1.5f, _c.Rb.linearVelocity.y); }
    public void Update() { _t -= Time.deltaTime; if (_t <= 0) _c.StateMachine.ChangeState(_c.ChaseState); }
    public void FixedUpdate() {} public void Exit() {}
}
