using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour {
        [SerializeField] private Image hpBarFill;
        [SerializeField] private Image staminaBarFill;
        [SerializeField] private TMPro.TextMeshProUGUI potionText;
    
        // 뚱뚱한 옛날 스크립트들 대신 통합 모듈 하나만 캐싱합니다.
        private PlayerStatsModule _stats;

        private void Start() {
                GameObject p = GameObject.FindGameObjectWithTag("Player");
                if (p) {
                        _stats = p.GetComponent<PlayerStatsModule>();
            
                        if (_stats) {
                                // 기력 바 UI 실시간 연동 (Action 대입)
                                _stats.OnStaminaChanged += (c, m) => { 
                                        if (staminaBarFill) staminaBarFill.fillAmount = c / m; 
                                };
                
                                // 포션 개수 UI 실시간 연동
                                _stats.OnPotionCountChanged += (c, m) => { 
                                        if (potionText) potionText.text = $"{c} / {m}"; 
                                };
                        }
                }
        }

        private void Update() {
                // 매 프레임 체력바 갱신 안전 처리
                if (_stats && hpBarFill) {
                        hpBarFill.fillAmount = _stats.CurrentHealth / _stats.MaxHealth;
                }
        }
}