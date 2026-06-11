using UnityEngine;

public class PlayerDieState : IState {
    private readonly PlayerController _c; public PlayerDieState(PlayerController c) => _c = c;
    public void Enter() { _c.Anim.Play(PlayerController.HashDeath); _c.Rb.linearVelocity = Vector2.zero; _c.Rb.bodyType = RigidbodyType2D.Static; }
    public void Update() {} public void FixedUpdate() {} public void Exit() {}
}
