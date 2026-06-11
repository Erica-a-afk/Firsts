using UnityEngine;
using System.Collections.Generic;

public class RoomTrigger : MonoBehaviour {
    [SerializeField] private int targetRoomIndex;
    [SerializeField] private GameObject[] monsterGroupPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    private readonly List<GameObject> _spawnedMonsters = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            StageProgressManager.Instance?.MoveToNextRoom(targetRoomIndex);
            ResetRoomMonsters();
        }
    }
    private void ResetRoomMonsters() {
        foreach (var m in _spawnedMonsters) { if (m != null) Destroy(m); }
        _spawnedMonsters.Clear();
        for (int i = 0; i < monsterGroupPrefabs.Length; i++) {
            if (i >= spawnPoints.Length) break;
            GameObject m = Instantiate(monsterGroupPrefabs[i], spawnPoints[i].position, Quaternion.identity);
            _spawnedMonsters.Add(m);
        }
    }
}
