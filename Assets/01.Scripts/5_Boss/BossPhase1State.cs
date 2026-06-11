using UnityEngine;

public class BossPhase1State : IState {
    private readonly AncientBossController _c; private float _t; public BossPhase1State(AncientBossController c) => _c = c;
    public void Enter() => _c.Anim.Play(AncientBossController.HashIdle);
    public void Update() { 
        _t += Time.deltaTime; 
        if (_t > 3f) { 
            _t = 0f; 
            _c.GetComponent<AncientBossPatternExecutor>()?.StartPattern("Attack");
        } 
    }
    public void FixedUpdate() {} public void Exit() {}
}
