using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWallSlideState : IState {
    private readonly PlayerController _c; public PlayerWallSlideState(PlayerController c) => _c = c;
    public void Enter() => _c.Anim.Play(PlayerController.HashWallSlide);
    public void Update() {
        var kb = Keyboard.current;
        if (_c.Movement.IsGrounded()) _c.StateMachine.ChangeState(_c.IdleState);
        if (!_c.Movement.IsTouchingWall()) _c.StateMachine.ChangeState(_c.FallState);
        if (kb != null && kb.spaceKey.wasPressedThisFrame) _c.StateMachine.ChangeState(_c.WallJumpState);
    }
    public void FixedUpdate() => _c.Rb.linearVelocity = new Vector2(_c.Rb.linearVelocity.x, Mathf.Max(_c.Rb.linearVelocity.y, -_c.Movement.wallSlideSpeed));
    public void Exit() {}
}
