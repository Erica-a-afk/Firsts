using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack1State : IState {
        private readonly PlayerController _c; 
        private float _t; 
    
        public PlayerAttack1State(PlayerController c) => _c = c;
    
        public void Enter() { 
                // _c.Stamina 대신 _c.Stats 기력 소모 매핑
                float cost = _c.Attack.AttackDataSO.combos[0].staminaCost;
                if(!_c.Stats.UseStamina(cost)) { 
                        _c.StateMachine.ChangeState(_c.IdleState); 
                        return; 
                } 
                _t = 0f; 
                _c.Attack.SetComboIndex(0); 
                _c.Anim.Play(PlayerController.HashSlash1); 
        }
    
        public void Update() { 
                _t += Time.deltaTime; 
                var mouse = Mouse.current; 
                if (mouse != null && _t > 0.12f && mouse.leftButton.wasPressedThisFrame) { 
                        _c.StateMachine.ChangeState(_c.Attack2State); 
                        return; 
                } 
                if (_t >= 0.3f) _c.StateMachine.ChangeState(_c.IdleState); 
        }
    
        public void FixedUpdate() {}
    
        public void Exit() {}
}