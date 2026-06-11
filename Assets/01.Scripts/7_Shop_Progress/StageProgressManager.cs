using UnityEngine;

public class StageProgressManager : MonoBehaviour {
    public static StageProgressManager Instance { get; private set; }
    public int CurrentRoomIndex { get; private set; } = 1;
    public bool HasDoubleJump { get; private set; } = false;
    public bool HasRoom3Key { get; private set; } = false;
    private Vector3 _returnPoint;
    private void Awake() { if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); } else Destroy(gameObject); }
    public void UnlockDoubleJump() => HasDoubleJump = true;
    public void ObtainRoom3Key() => HasRoom3Key = true;
    public void ResetDoubleJumpForAir() {}
    public void MoveToNextRoom(int idx) { CurrentRoomIndex = idx; }
    public void SaveWarp(Vector3 p) => _returnPoint = p;
    public Vector3 GetWarp() => _returnPoint;
    public float GetEnemyDamageMultiplier() => 1f + (CurrentRoomIndex - 1) * 0.3f;
    public int GetEnemyGoldReward(int baseGold) => Mathf.RoundToInt(baseGold * (1f + (CurrentRoomIndex - 1) * 0.5f));
}
