using UnityEngine;

public class PlayerStunState : IState {
    private readonly PlayerController _c; private float _t; public PlayerStunState(PlayerController c) => _c = c;
    public void Enter() { _t = 1.0f; _c.Anim.Play(PlayerController.HashHit); _c.Rb.linearVelocity = Vector2.zero; }
    public void Update() { _t -= Time.deltaTime; if (_t <= 0) _c.StateMachine.ChangeState(_c.IdleState); }
    public void FixedUpdate() {} public void Exit() {}
}
