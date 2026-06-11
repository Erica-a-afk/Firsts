using UnityEngine;

public class BossEnrageState : IState {
    private readonly AncientBossController _c; public BossEnrageState(AncientBossController c) => _c = c;
    public void Enter() => _c.Anim.Play(AncientBossController.HashBuff);
    public void Update() {} public void FixedUpdate() {} public void Exit() {}
}
