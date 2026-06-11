using UnityEngine;

public class PlayerLedgeClimbState : IState {
    private readonly PlayerController _c; private float _t; public PlayerLedgeClimbState(PlayerController c) => _c = c;
    public void Enter() { _t = 0.35f; _c.Anim.Play(PlayerController.HashLedgeGrab); _c.Rb.linearVelocity = Vector2.zero; }
    public void Update() { _t -= Time.deltaTime; if (_t <= 0) { _c.transform.position += new Vector3(_c.transform.localScale.x * 0.4f, 1.1f, 0f); _c.StateMachine.ChangeState(_c.IdleState); } }
    public void FixedUpdate() {} public void Exit() {}
}
