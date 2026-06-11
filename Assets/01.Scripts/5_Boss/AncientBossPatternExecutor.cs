using UnityEngine;
using System.Collections;

public class AncientBossPatternExecutor : MonoBehaviour {
    [Header("Indicator Prefabs Mapping")]
    [SerializeField] private GameObject attackIndicator;
    [SerializeField] private GameObject rangeIndicator;
    [SerializeField] private GameObject burstIndicator;
    [SerializeField] private GameObject spinIndicator;
    [SerializeField] private GameObject buffIndicator;

    private AncientBossController _c;
    private void Awake() => _c = GetComponent<AncientBossController>();

    public void StartPattern(string name) => StartCoroutine(name);

    private IEnumerator Attack() {
        attackIndicator.SetActive(true); yield return new WaitForSeconds(0.8f); attackIndicator.SetActive(false);
        _c.Anim.Play(AncientBossController.HashAttack);
        
        // 피드백 반영: PlayerHealth 대신 PlayerStatsModule을 가져옵니다.
        var stats = _c.Target?.GetComponent<PlayerStatsModule>();
        if (stats != null && stats.GetComponent<PlayerMovement>().IsGrounded()) stats.TakeDamage(20f);
    }

    private IEnumerator RangeAttack() {
        rangeIndicator.SetActive(true); yield return new WaitForSeconds(0.3f); rangeIndicator.SetActive(false);
        _c.Anim.Play(AncientBossController.HashRangeAttack);
        if (_c.Target && Vector2.Distance(transform.position, _c.Target.position) < 2f) {
            _c.Target.GetComponent<PlayerStatsModule>()?.TakeDamage(15f);
        }
    }

    private IEnumerator Burst() {
        burstIndicator.SetActive(true); CameraManager.Instance?.ShakeCamera(7f, 0.5f); yield return new WaitForSeconds(0.3f); burstIndicator.SetActive(false);
        _c.Anim.Play(AncientBossController.HashBurst);
        if (_c.Target && Vector2.Distance(transform.position, _c.Target.position) < 2.5f) {
            _c.Target.GetComponent<PlayerStatsModule>()?.TakeDamage(25f);
        }
    }

    private IEnumerator SpinAttack() {
        spinIndicator.SetActive(true); _c.Anim.Play(AncientBossController.HashSpinAttack);
        Vector3 targetPos = _c.Target.position; yield return new WaitForSeconds(0.6f); spinIndicator.SetActive(false);
        transform.position = new Vector3(targetPos.x, targetPos.y + 6f, transform.position.z);
        _c.Rb.linearVelocity = new Vector2(0f, -22f);
    }

    private IEnumerator Buff() {
        buffIndicator.SetActive(true); yield return new WaitForSeconds(1.0f); buffIndicator.SetActive(false);
        _c.Anim.Play(AncientBossController.HashBuff);
        if (_c.Target && Vector2.Distance(transform.position, _c.Target.position) < 4f) {
            _c.Target.GetComponent<PlayerStatsModule>()?.TakeDamage(30f);
        }
    }
}