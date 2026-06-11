using UnityEngine;

public class PlayerAttack4State : IState {
    private readonly PlayerController _c; private float _t; public PlayerAttack4State(PlayerController c) => _c = c;
    public void Enter() { _t = 0f; _c.Attack.SetComboIndex(3); _c.Anim.Play(PlayerController.HashRollAttack); }
    public void Update() { _t += Time.deltaTime; if (_t >= 0.5f) _c.StateMachine.ChangeState(_c.IdleState); }
    public void FixedUpdate() {}
    public void Exit() => _c.Attack.SetRollingAttackBonus(false);
}
