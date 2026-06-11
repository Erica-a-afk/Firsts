using UnityEngine;

public class EnemyPatrolRoute : MonoBehaviour {
    public Transform[] waypoints;
    public int GetNextIndex(int c) => waypoints.Length == 0 ? 0 : (c + 1) % waypoints.Length;
}
