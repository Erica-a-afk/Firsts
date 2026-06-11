using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
    public bool CheckInteractable(out Collider2D res) { 
        res = Physics2D.OverlapCircle(transform.position, 1.5f, LayerMask.GetMask("Interactable")); 
        return res != null; 
    }
}
