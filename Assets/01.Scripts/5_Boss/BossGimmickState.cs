using UnityEngine;

public class BossGimmickState : IState {
    private readonly AncientBossController _c; private float _t; public BossGimmickState(AncientBossController c) => _c = c;
    public void Enter() { _t = 2f; _c.GetComponent<AncientBossPatternExecutor>()?.StartPattern("Burst"); }
    public void Update() { _t -= Time.deltaTime; if (_t <= 0) _c.StateMachine.ChangeState(_c.Phase2State); }
    public void FixedUpdate() {} public void Exit() {}
}
