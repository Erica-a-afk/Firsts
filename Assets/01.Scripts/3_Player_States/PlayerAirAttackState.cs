using UnityEngine;

public class PlayerAirAttackState : IState {
    private readonly PlayerController _c; public PlayerAirAttackState(PlayerController c) => _c = c;
    public void Enter() { _c.Anim.Play(PlayerController.HashFallAttack); _c.Rb.linearVelocity = new Vector2(_c.Rb.linearVelocity.x, -7f); }
    public void Update() { if (_c.Movement.IsGrounded()) { CameraManager.Instance?.ShakeCamera(4f, 0.15f); _c.StateMachine.ChangeState(_c.LandState); } }
    public void FixedUpdate() {} public void Exit() {}
}
