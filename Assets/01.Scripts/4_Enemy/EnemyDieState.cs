using UnityEngine;

public class EnemyDieState : IState {
    private readonly EnemyController _c; public EnemyDieState(EnemyController c) => _c = c;
    public void Enter() { 
        _c.Anim.Play(EnemyController.HashDeath); _c.Rb.linearVelocity = Vector2.zero; _c.GetComponent<Collider2D>().enabled = false;
        if (_c.TryGetComponent(out IDamageable hp)) hp.Die();
    }
    public void Update() {} public void FixedUpdate() {} public void Exit() {}
}
