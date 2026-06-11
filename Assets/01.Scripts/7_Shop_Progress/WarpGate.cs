using UnityEngine;

public class WarpGate : MonoBehaviour {
        [SerializeField] private bool isShopExit = false;
    
        public void TriggerWarp(GameObject p) {
                if (!isShopExit) {
                        StageProgressManager.Instance?.SaveWarp(p.transform.position);
                        // 피드백 반영: 여기도 FindAnyObjectByType으로 변경하여 경고 원천 차단
                        Object.FindAnyObjectByType<SceneTransitionManager>()?.Load("ShopScene");
                } else {
                        Vector3 ret = StageProgressManager.Instance != null ? StageProgressManager.Instance.GetWarp() : Vector3.zero;
                        p.transform.position = ret;
                        Object.FindAnyObjectByType<SceneTransitionManager>()?.Load("MainStageScene");
                }
        }
}