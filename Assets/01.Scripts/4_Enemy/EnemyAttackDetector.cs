using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDetector : MonoBehaviour {
    private readonly List<IDamageable> _hist = new List<IDamageable>(); 
    public void Clear() => _hist.Clear();
    public IEnumerable<IDamageable> Detect(Vector2 c, Vector2 s, LayerMask l) {
        List<IDamageable> res = new List<IDamageable>();
        foreach (var col in Physics2D.OverlapBoxAll(c, s, 0f, l)) {
            if (col.TryGetComponent(out IDamageable t) && !_hist.Contains(t) && col.gameObject != gameObject) { 
                _hist.Add(t); res.Add(t); 
            }
        }
        return res;
    }
}
