using UnityEngine;

public class PlayerDashState : IState {
        private readonly PlayerController _c; 
        private float _t; 
    
        public PlayerDashState(PlayerController c) => _c = c;
    
        public void Enter() { 
                // _c.Stamina 대신 _c.Stats 기력 소모 15 레이어 호출
                if(!_c.Stats.UseStamina(15f)) { 
                        _c.StateMachine.ChangeState(_c.IdleState); 
                        return; 
                } 
                _t = _c.Movement.dashDuration; 
                _c.Anim.Play(PlayerController.HashDash); 
                _c.Rb.linearVelocity = new Vector2(_c.transform.localScale.x * _c.Movement.dashSpeed, 0f); 
                _c.Rb.gravityScale = 0f; 
        }
    
        public void Update() { 
                _t -= Time.deltaTime; 
                if (_t <= 0) _c.StateMachine.ChangeState(_c.IdleState); 
        }
    
        public void FixedUpdate() {}
    
        public void Exit() => _c.Rb.gravityScale = _c.Movement.originalGravityScale;
}