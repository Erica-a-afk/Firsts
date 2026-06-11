using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFallState : IState {
    private readonly PlayerController _c; public PlayerFallState(PlayerController c) => _c = c;
    public void Enter() => _c.Anim.Play(PlayerController.HashFall);
    public void Update() { 
        var kb = Keyboard.current;
        if (_c.Movement.IsGrounded()) _c.StateMachine.ChangeState(_c.LandState); 
        if (_c.Movement.IsTouchingWall()) _c.StateMachine.ChangeState(_c.WallSlideState);
        if (kb != null && kb.spaceKey.wasPressedThisFrame && StageProgressManager.Instance.HasDoubleJump) {
            StageProgressManager.Instance.ResetDoubleJumpForAir();
            _c.Rb.linearVelocity = new Vector2(_c.Rb.linearVelocity.x, _c.Movement.jumpForce);
            _c.StateMachine.ChangeState(_c.JumpState);
        }
    }
    public void FixedUpdate() => _c.Rb.linearVelocity = new Vector2(_c.Movement.GetInputX() * _c.Movement.moveSpeed * 0.8f, _c.Rb.linearVelocity.y);
    public void Exit() {}
}
