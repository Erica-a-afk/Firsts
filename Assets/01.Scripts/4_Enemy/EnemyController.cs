using UnityEngine;

public class EnemyController : MonoBehaviour {
    public StateMachine StateMachine { get; private set; } = new StateMachine();
    public Rigidbody2D Rb { get; private set; } 
    public Animator Anim { get; private set; } 
    public Transform Target { get; private set; }
    
    public float detectionRange = 5f, attackRange = 1.3f, moveSpeed = 2.5f;
    public float damageModifier = 1f;
    public int baseGoldReward = 20;

    public static readonly int HashIdle = Animator.StringToHash("Idle");
    public static readonly int HashMove = Animator.StringToHash("Move");
    public static readonly int HashAttack = Animator.StringToHash("Attack");
    public static readonly int HashHit = Animator.StringToHash("hit");
    public static readonly int HashDeath = Animator.StringToHash("death");

    public EnemyIdleState IdleState { get; private set; } 
    public EnemyPatrolState PatrolState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; } 
    public EnemyAttackState AttackState { get; private set; }
    public EnemyHitState HitState { get; private set; } 
    public EnemyDieState DieState { get; private set; }

    private void Awake() {
        Rb = GetComponent<Rigidbody2D>(); 
        Anim = GetComponentInChildren<Animator>();
        
        IdleState = new EnemyIdleState(this); PatrolState = new EnemyPatrolState(this); ChaseState = new EnemyChaseState(this);
        AttackState = new EnemyAttackState(this); HitState = new EnemyHitState(this); DieState = new EnemyDieState(this);
    }

    private void Start() { 
        var p = GameObject.FindGameObjectWithTag("Player"); 
        if (p) Target = p.transform; 
        
        if (StageProgressManager.Instance != null) {
            damageModifier = StageProgressManager.Instance.GetEnemyDamageMultiplier();
        }
        StateMachine.ChangeState(IdleState); 
    }

    private void Update() => StateMachine.Update(); 
    private void FixedUpdate() => StateMachine.FixedUpdate();
    
    public bool CanSeePlayer() => Target && Vector2.Distance(transform.position, Target.position) <= detectionRange;
    public bool CanAttackPlayer() => Target && Vector2.Distance(transform.position, Target.position) <= attackRange;
}
