using UnityEngine;

public class PlayerUltimateState : IState {
    private readonly PlayerController _c; private float _t; public PlayerUltimateState(PlayerController c) => _c = c;
    public void Enter() { _t = 0.7f; _c.Anim.Play(PlayerController.HashSlam); _c.Rb.linearVelocity = Vector2.zero; CameraManager.Instance?.ShakeCamera(6f, 0.3f); }
    public void Update() { _t -= Time.deltaTime; if (_t <= 0) _c.StateMachine.ChangeState(_c.IdleState); }
    public void FixedUpdate() {} public void Exit() {}
}
