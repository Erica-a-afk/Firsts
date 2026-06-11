using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRollState : IState {
        private readonly PlayerController _c; 
        private float _t; 
    
        public PlayerRollState(PlayerController c) => _c = c;
    
        public void Enter() { 
                // _c.Stamina 대신 _c.Stats 기력 소모 연산 호출
                if(!_c.Stats.UseStamina(20f)) { 
                        _c.StateMachine.ChangeState(_c.IdleState); 
                        return; 
                } 
                _t = 0.4f; 
                _c.Anim.Play(PlayerController.HashRoll); 
                _c.Rb.linearVelocity = new Vector2(_c.transform.localScale.x * _c.Movement.moveSpeed * 1.4f, _c.Rb.linearVelocity.y); 
        
                // _c.Health 대신 _c.Stats 무적 스위치 오작동 방지 가동
                _c.Stats.SetInvincible(true); 
        }
    
        public void Update() { 
                _t -= Time.deltaTime; 
                var mouse = Mouse.current;
                if (mouse != null && mouse.leftButton.wasPressedThisFrame) { 
                        _c.Attack.SetRollingAttackBonus(true); 
                        _c.StateMachine.ChangeState(_c.Attack4State); 
                        return; 
                } 
                if (_t <= 0) _c.StateMachine.ChangeState(_c.IdleState); 
        }
    
        public void FixedUpdate() {}
    
        public void Exit() => _c.Stats.SetInvincible(false);
}