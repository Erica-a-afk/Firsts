using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack2State : IState {
        private readonly PlayerController _c; 
        private float _t; 
    
        public PlayerAttack2State(PlayerController c) => _c = c;
    
        public void Enter() { 
                // 뚱뚱한 컴포넌트 해제 반영: _c.Stamina 대신 통합 _c.Stats 사용
                float cost = _c.Attack.AttackDataSO.combos[1].staminaCost;
                if(!_c.Stats.UseStamina(cost)) { 
                        _c.StateMachine.ChangeState(_c.IdleState); 
                        return; 
                } 
                _t = 0f; 
                _c.Attack.SetComboIndex(1); // 콤보 인덱스 1번(2타) 지정
                _c.Anim.Play(PlayerController.HashSlash2); 
        }
    
        public void Update() { 
                _t += Time.deltaTime; 
                var mouse = Mouse.current; 
        
                // 0.12초가 지난 시점에 마우스 왼쪽 버튼을 다시 누르면 막타인 3타 상태로 전이
                if (mouse != null && _t > 0.12f && mouse.leftButton.wasPressedThisFrame) { 
                        _c.StateMachine.ChangeState(_c.Attack3State); 
                        return; 
                } 
                if (_t >= 0.3f) _c.StateMachine.ChangeState(_c.IdleState); 
        }
    
        public void FixedUpdate() {}
    
        public void Exit() {}
}