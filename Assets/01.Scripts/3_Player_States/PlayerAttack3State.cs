using UnityEngine;

public class PlayerAttack3State : IState {
        private readonly PlayerController _c; 
        private float _t; 
    
        public PlayerAttack3State(PlayerController c) => _c = c;
    
        public void Enter() { 
                // 뚱뚱한 컴포넌트 해제 반영: _c.Stamina 대신 통합 _c.Stats 사용
                float cost = _c.Attack.AttackDataSO.combos[2].staminaCost;
                if(!_c.Stats.UseStamina(cost)) { 
                        _c.StateMachine.ChangeState(_c.IdleState); 
                        return; 
                } 
                _t = 0f; 
                _c.Attack.SetComboIndex(2); // 콤보 인덱스 2번(3타 대형 내리치기) 지정
                _c.Anim.Play(PlayerController.HashSlam); 
        }
    
        public void Update() { 
                _t += Time.deltaTime; 
                // 3타는 콤보의 마지막 강력한 일격이므로 다음 연계 없이 선딜/후딜레이가 끝나면 자동으로 Idle 상태로 복귀합니다.
                if (_t >= 0.45f) _c.StateMachine.ChangeState(_c.IdleState); 
        }
    
        public void FixedUpdate() {}
    
        public void Exit() {}
}