using UnityEngine;

public class PlayerWallJumpState : IState {
    private readonly PlayerController _c; private float _t; public PlayerWallJumpState(PlayerController c) => _c = c;
    public void Enter() { _t = 0.18f; _c.Anim.Play(PlayerController.HashJump); float d = _c.transform.localScale.x * -1f; _c.transform.localScale = new Vector3(d, 1f, 1f); _c.Rb.linearVelocity = new Vector2(d * _c.Movement.wallJumpForce.x, _c.Movement.wallJumpForce.y); }
    public void Update() { _t -= Time.deltaTime; if (_t <= 0) _c.StateMachine.ChangeState(_c.FallState); }
    public void FixedUpdate() {} public void Exit() {}
}
