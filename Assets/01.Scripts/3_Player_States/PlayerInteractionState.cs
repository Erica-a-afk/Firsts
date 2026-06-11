using UnityEngine;

public class PlayerInteractionState : IState {
    private readonly PlayerController _c; private float _t; public PlayerInteractionState(PlayerController c) => _c = c;
    public void Enter() { _t = 0.4f; _c.Anim.Play(PlayerController.HashTrans); }
    public void Update() { _t -= Time.deltaTime; if (_t <= 0) _c.StateMachine.ChangeState(_c.IdleState); }
    public void FixedUpdate() {} public void Exit() {}
}
