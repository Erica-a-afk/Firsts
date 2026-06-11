using UnityEngine;

// 기존의 체력, 스태미나, 지갑, 포션 인벤토리, 스탯 매니저를 하나로 묶은 데이터 코어
public class PlayerStatsModule : MonoBehaviour, IDamageable {
    [Header("기본 에셋 데이터 시트 링크")]
    [SerializeField] private DamageAreaSO statsSO;

    [Header("가변 데이터 스탯 (체력 / 기력 / 재화)")]
    public float maxHealth = 100f;
    public float maxStamina = 100f;
    [SerializeField] private float staminaRegenRate = 25f;
    [SerializeField] private float potionHealAmount = 30f;

    public float CurrentHealth { get; set; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float CurrentStamina { get; private set; }
    public int CurrentPotions { get; private set; } = 3;
    public int MaxPotions { get; private set; } = 3;
    public int Gold { get; private set; }

    [Header("상점 영구 누적 가산 스탯")]
    public float bonusDamage;
    public float bonusMaxStamina;

    public float FinalDamage => (statsSO ? statsSO.baseDamage : 10f) + bonusDamage;

    private bool _isInvincible;
    private bool _isGuardActive;
    private bool _isParryActive;
    public bool IsParrySuccess { get; private set; }

    public System.Action<float, float> OnStaminaChanged;
    public System.Action<int, int> OnPotionCountChanged;
    public System.Action<int> OnGoldChanged;

    private void Start() {
        CurrentHealth = maxHealth;
        CurrentStamina = maxStamina;
        OnPotionCountChanged?.Invoke(CurrentPotions, MaxPotions);
        OnGoldChanged?.Invoke(Gold);
    }

    private void Update() {
        // 기력 자연 회복 연산
        float staminaLimit = maxStamina + bonusMaxStamina;
        if (CurrentStamina < staminaLimit) {
            CurrentStamina = Mathf.Min(staminaLimit, CurrentStamina + staminaRegenRate * Time.deltaTime);
            OnStaminaChanged?.Invoke(CurrentStamina, staminaLimit);
        }
    }

    public void SetInvincible(bool state) => _isInvincible = state;
    public void SetGuardActive(bool state) => _isGuardActive = state;
    public void SetParryWindowActive(bool state) { _isParryActive = state; if (state) IsParrySuccess = false; }
    public void TriggerParrySuccess() => IsParrySuccess = true;

    public void TakeDamage(float dmg) {
        if (_isInvincible) return;
        if (_isParryActive) { TriggerParrySuccess(); return; }
        if (_isGuardActive) dmg *= 0.2f; // 가드 성공 시 대미지 80% 경감

        CurrentHealth -= dmg;
        var ctrl = GetComponent<PlayerController>();
        
        if (CurrentHealth <= 0f) ctrl.StateMachine.ChangeState(ctrl.DieState);
        else if (!_isGuardActive) ctrl.StateMachine.ChangeState(ctrl.HitState);
    }

    public bool UseStamina(float amount) {
        if (CurrentStamina < amount) return false;
        CurrentStamina -= amount;
        OnStaminaChanged?.Invoke(CurrentStamina, maxStamina + bonusMaxStamina);
        return true;
    }

    public void AddGold(int amount) { Gold += amount; OnGoldChanged?.Invoke(Gold); }
    public bool TrySpendGold(int amount) {
        if (Gold < amount) return false;
        Gold -= amount; OnGoldChanged?.Invoke(Gold); return true;
    }

    public void UsePotion() {
        if (CurrentPotions <= 0 || CurrentHealth >= maxHealth) return;
        CurrentPotions--;
        CurrentHealth = Mathf.Min(maxHealth, CurrentHealth + potionHealAmount);
        OnPotionCountChanged?.Invoke(CurrentPotions, MaxPotions);
        SoundManager.Instance?.PlaySFX("Heal");
    }

    public bool TryAddPotion() {
        if (CurrentPotions >= MaxPotions) return false;
        CurrentPotions++;
        OnPotionCountChanged?.Invoke(CurrentPotions, MaxPotions);
        return true;
    }

    public void UpgradeAttack(float value) => bonusDamage += value;
    public void UpgradeStamina(float value) => bonusMaxStamina += value;
    public void Die() => gameObject.SetActive(false);
}