using UnityEngine;

public class AncientBossController : MonoBehaviour {
    public StateMachine StateMachine { get; private set; } = new StateMachine();
    public Animator Anim { get; private set; } 
    public Rigidbody2D Rb { get; private set; } 
    public Transform Target { get; private set; }
    
    public BossPhase1State Phase1State { get; private set; } 
    public BossPhase2State Phase2State { get; private set; }
    public BossGimmickState GimmickState { get; private set; } 
    public BossEnrageState EnrageState { get; private set; }
    
    public float phase2Threshold = 0.5f;

    public static readonly int HashIdle = Animator.StringToHash("Idle");
    public static readonly int HashMove = Animator.StringToHash("Move");
    public static readonly int HashAttack = Animator.StringToHash("Attack");
    public static readonly int HashRangeAttack = Animator.StringToHash("Range Attack");
    public static readonly int HashSpinAttack = Animator.StringToHash("Spin Attack");
    public static readonly int HashBuff = Animator.StringToHash("Buff");
    public static readonly int HashBurst = Animator.StringToHash("Burst");
    public static readonly int HashDeath = Animator.StringToHash("Death");

    private void Awake() {
        Anim = GetComponentInChildren<Animator>(); 
        Rb = GetComponent<Rigidbody2D>();
        Phase1State = new BossPhase1State(this); Phase2State = new BossPhase2State(this); 
        GimmickState = new BossGimmickState(this); EnrageState = new BossEnrageState(this);
    }
    private void Start() { 
        var p = GameObject.FindGameObjectWithTag("Player"); if (p) Target = p.transform; 
        StateMachine.ChangeState(Phase1State); 
    }
    private void Update() => StateMachine.Update();
}
