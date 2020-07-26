using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour {

    [SerializeField] private Transform SpawnPointParent;
    [SerializeField] private SpawnPoint[] spawnPoints;

    [SerializeField] private GameObject[] obstacles;
    private GameObject obstacle;

    private void OnValidate() {
        spawnPoints = SpawnPointParent.GetComponentsInChildren<SpawnPoint>();
    }

    private void Start() {
        obstacle = obstacles[Random.Range(0, obstacles.Length)];
        foreach(SpawnPoint spawn in spawnPoints) {
            spawn.SetObstacle(obstacle);
        }
    }

    public void SetObstacles(GameObject[] obstacles) {
        this.obstacles = obstacles;
    }
}
