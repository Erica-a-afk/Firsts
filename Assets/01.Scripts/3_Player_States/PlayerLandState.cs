using UnityEngine;

public class PlayerLandState : IState {
    private readonly PlayerController _c; private float _t; public PlayerLandState(PlayerController c) => _c = c;
    public void Enter() { _t = 0.1f; _c.Anim.Play(PlayerController.HashCrouchLand); _c.Rb.linearVelocity = Vector2.zero; }
    public void Update() { _t -= Time.deltaTime; if (_t <= 0) _c.StateMachine.ChangeState(_c.IdleState); }
    public void FixedUpdate() {}
    public void Exit() {}
}
