using UnityEngine;

public class PlayerHitState : IState {
    private readonly PlayerController _c; private float _t; public PlayerHitState(PlayerController c) => _c = c;
    public void Enter() { _t = 0.25f; _c.Anim.Play(PlayerController.HashHit); _c.Rb.linearVelocity = new Vector2(_c.transform.localScale.x * -2.5f, 2f); }
    public void Update() { _t -= Time.deltaTime; if (_t <= 0) _c.StateMachine.ChangeState(_c.IdleState); }
    public void FixedUpdate() {}
    public void Exit() {}
}
