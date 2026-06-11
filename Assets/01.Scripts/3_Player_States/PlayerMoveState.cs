using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveState : IState {
    private readonly PlayerController _c; public PlayerMoveState(PlayerController c) => _c = c;
    public void Enter() => _c.Anim.Play(PlayerController.HashWalk);
    public void Update() {
        var kb = Keyboard.current; var mouse = Mouse.current; if (kb == null || mouse == null) return;
        float x = _c.Movement.GetInputX(); if (x == 0) { _c.StateMachine.ChangeState(_c.IdleState); return; }
    
        if (kb.spaceKey.wasPressedThisFrame) _c.StateMachine.ChangeState(_c.JumpState);
        if (mouse.leftButton.wasPressedThisFrame) _c.StateMachine.ChangeState(_c.Attack1State); // 마우스 클릭 공격
        if (kb.qKey.wasPressedThisFrame) _c.StateMachine.ChangeState(_c.RollState);              // Q 구르기
        if (kb.eKey.wasPressedThisFrame) _c.StateMachine.ChangeState(_c.DashState);             // E 대시
    
        _c.transform.localScale = new Vector3(x > 0 ? 1f : -1f, 1f, 1f);
    }
    public void FixedUpdate() => _c.Rb.linearVelocity = new Vector2(_c.Movement.GetInputX() * _c.Movement.moveSpeed, _c.Rb.linearVelocity.y);
    public void Exit() {}
}
