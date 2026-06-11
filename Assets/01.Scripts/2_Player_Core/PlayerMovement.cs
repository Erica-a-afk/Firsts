using UnityEngine;
using UnityEngine.InputSystem; // 피드백 주신 인풋 시스템 단축 반영

public class PlayerMovement : MonoBehaviour {
        [Header("물리 기동 스펙")]
        public float moveSpeed = 5f;
        public float jumpForce = 12f;
        public float dashSpeed = 16f;
        public float dashDuration = 0.2f;
        public float wallSlideSpeed = 2f;
        public float originalGravityScale = 3f;
        public Vector2 wallJumpForce = new Vector2(6f, 12f);

        [Header("센서 레이더 매핑")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private Transform wallCheck;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask wallLayer;
    
        private BoxCollider2D _parentCollider; // 최상단 부모 루트 본체의 물리 콜라이더 참조
        private float _origH;

        private void Awake() {
                // 자식에서 부모에 붙은 본체 콜라이더를 역추적하여 캐싱
                _parentCollider = GetComponentInParent<BoxCollider2D>();
                if (_parentCollider != null) _origH = _parentCollider.size.y;
        }

        public float GetInputX() {
                if (Keyboard.current == null) return 0f;
                float x = 0f;
                if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) x = -1f;
                if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) x = 1f;
                return x;
        }
    
        // 피드백 주신 단일 Collider 최적화 원라인 체크 방식
        public bool IsGrounded() => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer) != null;
        public bool IsTouchingWall() => Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer) != null;
    
        // 부모 본체의 물리 충돌 박스 높이를 조절 (구르기, 앉기 기믹용)
        public void SetColliderHeight(float f) { if (_parentCollider) _parentCollider.size = new Vector2(_parentCollider.size.x, _origH * f); }
        public void ResetColliderHeight() { if (_parentCollider) _parentCollider.size = new Vector2(_parentCollider.size.x, _origH); }
}