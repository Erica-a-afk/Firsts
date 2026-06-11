using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLedgeGrabState : IState {
    private readonly PlayerController _c; public PlayerLedgeGrabState(PlayerController c) => _c = c;
    public void Enter() { _c.Anim.Play(PlayerController.HashLedgeGrab); _c.Rb.linearVelocity = Vector2.zero; _c.Rb.gravityScale = 0f; }
    public void Update() { var kb = Keyboard.current; if (kb == null) return; if (kb.sKey.wasPressedThisFrame) _c.StateMachine.ChangeState(_c.FallState); if (kb.wKey.wasPressedThisFrame) _c.StateMachine.ChangeState(_c.LedgeClimbState); }
    public void FixedUpdate() {}
    public void Exit() => _c.Rb.gravityScale = _c.Movement.originalGravityScale;
}
