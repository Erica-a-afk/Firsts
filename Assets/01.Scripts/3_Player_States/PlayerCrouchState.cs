using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCrouchState : IState {
    private readonly PlayerController _c; public PlayerCrouchState(PlayerController c) => _c = c;
    public void Enter() { _c.Anim.Play(PlayerController.HashCrouchLand); _c.Movement.SetColliderHeight(0.5f); }
    public void Update() { var kb = Keyboard.current; if (kb != null && kb.sKey.wasReleasedThisFrame) _c.StateMachine.ChangeState(_c.IdleState); }
    public void FixedUpdate() => _c.Rb.linearVelocity = new Vector2(0f, _c.Rb.linearVelocity.y);
    public void Exit() => _c.Movement.ResetColliderHeight();
}
