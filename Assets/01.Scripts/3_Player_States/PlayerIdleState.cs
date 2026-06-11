using UnityEngine;
using UnityEngine.InputSystem; // 마우스 및 키보드 입력용

public class PlayerIdleState : IState {
        private readonly PlayerController _c; 
        public PlayerIdleState(PlayerController c) => _c = c;

        public void Enter() => _c.Anim.Play(PlayerController.HashIdle);

        public void Update() {
                var kb = Keyboard.current; 
                var mouse = Mouse.current;
                if (kb == null || mouse == null) return;

                // 1. 이동 및 점프 (Space 통합) 감지
                if (_c.Movement.GetInputX() != 0) _c.StateMachine.ChangeState(_c.MoveState);
                if (kb.spaceKey.wasPressedThisFrame && _c.Movement.IsGrounded()) _c.StateMachine.ChangeState(_c.JumpState);
        
                // 2. 피드백 반영: 마우스 왼쪽 클릭 시 공격 1타 스타트!
                if (mouse.leftButton.wasPressedThisFrame) _c.StateMachine.ChangeState(_c.Attack1State);
        
                // 3. 피드백 반영: Q 키 구르기 / E 키 대시 매핑 완료
                if (kb.qKey.wasPressedThisFrame) _c.StateMachine.ChangeState(_c.RollState);
                if (kb.eKey.wasPressedThisFrame) _c.StateMachine.ChangeState(_c.DashState);
        
                // 4. 가드, 앉기, 상호작용
                if (kb.xKey.wasPressedThisFrame) _c.StateMachine.ChangeState(_c.ParryState);
                if (kb.sKey.isPressed) _c.StateMachine.ChangeState(_c.CrouchState);
                if (kb.fKey.wasPressedThisFrame) {
                        if (_c.GetComponent<PlayerInteraction>().CheckInteractable(out Collider2D res)) {
                                if (res.TryGetComponent(out InteractionObject io)) io.Interact();
                                else if (res.TryGetComponent(out WarpGate wg)) wg.TriggerWarp(_c.gameObject);
                                _c.StateMachine.ChangeState(_c.InteractionState);
                        }
                }
        }

        public void FixedUpdate() => _c.Rb.linearVelocity = new Vector2(0, _c.Rb.linearVelocity.y);
        public void Exit() {}
}