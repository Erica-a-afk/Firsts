using UnityEngine;

public class BossPhase2State : IState {
    private readonly AncientBossController _c; private float _t; public BossPhase2State(AncientBossController c) => _c = c;
    public void Enter() => _c.Anim.Play(AncientBossController.HashIdle);
    public void Update() { 
        _t += Time.deltaTime; 
        if (_t > 2.5f) { 
            _t = 0f; 
            string[] patterns = { "RangeAttack", "SpinAttack", "Buff" };
            _c.GetComponent<AncientBossPatternExecutor>()?.StartPattern(patterns[Random.Range(0, 3)]);
        } 
    }
    public void FixedUpdate() {} public void Exit() {}
}
