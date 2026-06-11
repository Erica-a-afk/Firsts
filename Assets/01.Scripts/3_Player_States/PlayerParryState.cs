using UnityEngine;

public class PlayerParryState : IState {
        private readonly PlayerController _c; 
        private float _t; 
    
        public PlayerParryState(PlayerController c) => _c = c;
    
        public void Enter() { 
                _t = 0.15f; 
                _c.Anim.Play(PlayerController.HashBlock); 
        
                // 피드백 반영: _c.Health 대신 통합 모듈인 _c.Stats를 바라봅니다.
                _c.Stats.SetParryWindowActive(true); 
        }
    
        public void Update() { 
                _t -= Time.deltaTime; 
                if (_c.Stats.IsParrySuccess) { 
                        _c.StateMachine.ChangeState(_c.UltimateState); 
                        return; 
                } 
                if (_t <= 0) _c.StateMachine.ChangeState(_c.GuardState); 
        }
    
        public void FixedUpdate() {}
    
        public void Exit() => _c.Stats.SetParryWindowActive(false);
}