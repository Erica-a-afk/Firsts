using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGuardState : IState {
        private readonly PlayerController _c; 
        public PlayerGuardState(PlayerController c) => _c = c;
    
        public void Enter() { 
                _c.Anim.Play(PlayerController.HashBlock); 
                _c.Rb.linearVelocity = Vector2.zero; 
        
                // _c.Health 대신 _c.Stats 가드 활성화
                _c.Stats.SetGuardActive(true); 
        }
    
        public void Update() { 
                var kb = Keyboard.current; 
                if (kb != null && kb.xKey.wasReleasedThisFrame) _c.StateMachine.ChangeState(_c.IdleState); 
        }
    
        public void FixedUpdate() {}
    
        public void Exit() => _c.Stats.SetGuardActive(false);
}