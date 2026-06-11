using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumpState : IState {
    private readonly PlayerController _c; private bool _canDoubleJump;
    public PlayerJumpState(PlayerController c) => _c = c;
    public void Enter() { 
        _c.Anim.Play(PlayerController.HashJump); 
        _c.Rb.linearVelocity = new Vector2(_c.Rb.linearVelocity.x, _c.Movement.jumpForce);
        _canDoubleJump = StageProgressManager.Instance.HasDoubleJump;
    }
    public void Update() { 
        var kb = Keyboard.current;
        if (kb != null && kb.spaceKey.wasPressedThisFrame && _canDoubleJump) {
            _canDoubleJump = false; // 공중 도약 기회 소모
            _c.Rb.linearVelocity = new Vector2(_c.Rb.linearVelocity.x, _c.Movement.jumpForce);
            _c.Anim.Play(PlayerController.HashJump, -1, 0f);
        }
        if (_c.Rb.linearVelocity.y <= 0) _c.StateMachine.ChangeState(_c.FallState); 
    }
    public void FixedUpdate() => _c.Rb.linearVelocity = new Vector2(_c.Movement.GetInputX() * _c.Movement.moveSpeed * 0.8f, _c.Rb.linearVelocity.y);
    public void Exit() {}
}
