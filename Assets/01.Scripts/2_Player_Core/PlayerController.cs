using UnityEngine;

// 최상단 Player 오브젝트에 부착되는 컨트롤러 메인 허브입니다.
public class PlayerController : MonoBehaviour {
    public StateMachine StateMachine { get; private set; } = new StateMachine();
    
    // 연결 컴포넌트 캐싱 라인
    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerMovement Movement { get; private set; }
    public PlayerAttack Attack { get; private set; }
    public PlayerStatsModule Stats { get; private set; } // 통합 스탯 모듈

    #region 애니메이션 해시 사전 선언 (유니티 6 사양 완전 복구)
    public static readonly int HashIdle = Animator.StringToHash("Idle");
    public static readonly int HashWalk = Animator.StringToHash("Walk");
    public static readonly int HashJump = Animator.StringToHash("Jump");
    public static readonly int HashFall = Animator.StringToHash("Fall");
    public static readonly int HashDash = Animator.StringToHash("Dash");
    public static readonly int HashRoll = Animator.StringToHash("Roll");
    public static readonly int HashCrouchLand = Animator.StringToHash("crouch land");
    public static readonly int HashHit = Animator.StringToHash("Hit");
    public static readonly int HashDeath = Animator.StringToHash("death");
    public static readonly int HashBlock = Animator.StringToHash("Block");
    public static readonly int HashSlash1 = Animator.StringToHash("Slash 1");
    public static readonly int HashSlash2 = Animator.StringToHash("Slash 2");
    public static readonly int HashSlam = Animator.StringToHash("Slam");
    public static readonly int HashRollAttack = Animator.StringToHash("Roll Attack");
    public static readonly int HashFallAttack = Animator.StringToHash("Fall Attack");
    public static readonly int HashWallSlide = Animator.StringToHash("Wall Slide");
    public static readonly int HashLedgeGrab = Animator.StringToHash("Ledge Grab");
    public static readonly int HashTrans = Animator.StringToHash("trans"); // 에러 원인이었던 해시 완벽 복구!
    #endregion

    #region 24종 전체 상태(State) 인스턴스 슬롯 완벽 복구
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerRollState RollState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerHitState HitState { get; private set; }
    public PlayerDieState DieState { get; private set; }
    public PlayerAttack1State Attack1State { get; private set; }
    public PlayerAttack2State Attack2State { get; private set; }
    public PlayerAttack3State Attack3State { get; private set; }
    public PlayerAttack4State Attack4State { get; private set; }
    public PlayerAirAttackState AirAttackState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeGrabState LedgeGrabState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerCrouchState CrouchState { get; private set; }
    public PlayerStunState StunState { get; private set; }
    public PlayerGuardState GuardState { get; private set; }
    public PlayerParryState ParryState { get; private set; }
    public PlayerUltimateState UltimateState { get; private set; }
    public PlayerInteractionState InteractionState { get; private set; }
    #endregion

    private void Awake() {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponentInChildren<Animator>();
        Movement = GetComponentInChildren<PlayerMovement>();
        Attack = GetComponentInChildren<PlayerAttack>();
        Stats = GetComponent<PlayerStatsModule>();
        
        InitializeStates();
    }

    private void InitializeStates() {
        IdleState = new PlayerIdleState(this); MoveState = new PlayerMoveState(this); JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this); DashState = new PlayerDashState(this); RollState = new PlayerRollState(this);
        LandState = new PlayerLandState(this); HitState = new PlayerHitState(this); DieState = new PlayerDieState(this);
        Attack1State = new PlayerAttack1State(this); Attack2State = new PlayerAttack2State(this); Attack3State = new PlayerAttack3State(this);
        Attack4State = new PlayerAttack4State(this); AirAttackState = new PlayerAirAttackState(this); WallSlideState = new PlayerWallSlideState(this);
        WallJumpState = new PlayerWallJumpState(this); LedgeGrabState = new PlayerLedgeGrabState(this); LedgeClimbState = new PlayerLedgeClimbState(this);
        CrouchState = new PlayerCrouchState(this); StunState = new PlayerStunState(this); GuardState = new PlayerGuardState(this);
        ParryState = new PlayerParryState(this); UltimateState = new PlayerUltimateState(this); InteractionState = new PlayerInteractionState(this);
    }

    private void Start() => StateMachine.ChangeState(IdleState);
    private void Update() => StateMachine.Update();
    private void FixedUpdate() => StateMachine.FixedUpdate();
}