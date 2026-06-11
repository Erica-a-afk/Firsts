using UnityEngine;

public class BossShield : MonoBehaviour {
    public bool IsActive { get; private set; }
    public void Toggle(bool s) => IsActive = s;
}
