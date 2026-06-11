using UnityEngine;

public class ShopManager : MonoBehaviour {
        public static ShopManager Instance { get; private set; }
        [SerializeField] private int upgradeCost = 50, potionCost = 20;
    
        private void Awake() => Instance = this;

        // 공격력 영구 강화 구매 버튼 이벤트
        public void BuyAttack(GameObject p) {
                if (p.TryGetComponent(out PlayerStatsModule stats) && stats.TrySpendGold(upgradeCost)) {
                        stats.UpgradeAttack(2f);
                }
        }

        // 포션 재고 보충 구매 버튼 이벤트
        public void BuyPotion(GameObject p) {
                if (p.TryGetComponent(out PlayerStatsModule stats) && stats.CurrentPotions < 3 && stats.TrySpendGold(potionCost)) {
                        stats.TryAddPotion();
                }
        }
}