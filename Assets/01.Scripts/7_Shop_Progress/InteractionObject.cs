using UnityEngine;

public class InteractionObject : MonoBehaviour {
    public enum TargetObjectType { StonePillar, ChestRoom3 }
    public TargetObjectType objectType;
    private bool _used = false;
    public void Interact() {
        if (_used) return;
        if (objectType == TargetObjectType.StonePillar) { StageProgressManager.Instance?.UnlockDoubleJump(); _used = true; }
        else if (objectType == TargetObjectType.ChestRoom3) { StageProgressManager.Instance?.ObtainRoom3Key(); _used = true; }
    }
}
