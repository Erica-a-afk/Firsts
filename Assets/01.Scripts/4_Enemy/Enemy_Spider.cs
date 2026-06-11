using UnityEngine;

public class Enemy_Spider : MonoBehaviour {
    public enum SpiderState { Sleep, Wake, Move, Hit, Death }
    public SpiderState currentState = SpiderState.Sleep;
    [SerializeField] private float wakeUpRadius = 3f;
    private Transform _player; private Animator _anim;

    private static readonly int HashSleep = Animator.StringToHash("Sleep");
    private static readonly int HashWake = Animator.StringToHash("Wake");
    private static readonly int HashMove = Animator.StringToHash("Move");

    private void Awake() => _anim = GetComponentInChildren<Animator>();
    private void Start() {
        var p = GameObject.FindGameObjectWithTag("Player"); if (p) _player = p.transform;
        _anim.Play(HashSleep);
    }
    private void Update() {
        if (currentState == SpiderState.Sleep && _player != null) {
            if (Vector2.Distance(transform.position, _player.position) <= wakeUpRadius) {
                StartCoroutine(WakeUpSequence());
            }
        }
    }
    private System.Collections.IEnumerator WakeUpSequence() {
        currentState = SpiderState.Wake; _anim.Play(HashWake);
        yield return new WaitForSeconds(0.5f);
        currentState = SpiderState.Move; _anim.Play(HashMove);
        if (TryGetComponent(out EnemyController ec)) ec.StateMachine.ChangeState(ec.ChaseState);
    }
}
