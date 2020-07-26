using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField] private GameObject[] obstaclePatterns;

    [SerializeField] private List<GameObject> obstaclesTemplate;
    private GameObject[] obstacles;

    private int activeIndex = -1;
    [SerializeField] private AnimationCurve spawnCurve;
    [SerializeField] private float curveLength = 180f;
    private float curveVal;
    private float currTime = 0;
    private float startTime = 0;

    [SerializeField] private float minSpeed = 3f;
    [SerializeField] private float maxSpeed = 7f;
    private float currSpeed;
    [SerializeField] private float speedInterval = 30f;
    private float currTime_speed = 0f;

    private int i;
    private int j;

    private GameObject obstacle;

    private void Start() {
        currTime_speed = speedInterval;
        currSpeed = minSpeed;
        currTime = 0;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update() {
        if (currTime <= 0) {
            i = Random.Range(0, obstaclePatterns.Length);
            obstaclePatterns[i].GetComponent<ObstacleManager>().SetObstacles(obstacles);
            Instantiate(obstaclePatterns[i], transform.position, Quaternion.identity);

            curveVal = spawnCurve.Evaluate(Mathf.Min((Time.time - startTime) / curveLength, 1));
            currTime = curveVal;
        } else {
            currTime -= Time.deltaTime;
        }

        // if (currTime_speed <= 0) {
        //     currSpeed += 0.2f;
        //     if (currSpeed > maxSpeed) currSpeed = maxSpeed;

        //     foreach(GameObject obstacle in obstacles) {
        //         obstacle.GetComponent<Obstacle>().SetSpeed(currSpeed);
        //     }

        //     currTime_speed = speedInterval;
        // } else {
        //     currTime_speed -= Time.deltaTime;
        // }
        
    }

    public void Select(int index) {
        if (index == activeIndex) return;

        if (index == 0) {
            obstacles = obstaclesTemplate.GetRange(0, 2).ToArray();
        } else if (index == 1) {
            obstacles = obstaclesTemplate.GetRange(2, 2).ToArray();
        } else if (index == 2) {
            obstacles = obstaclesTemplate.GetRange(4, 2).ToArray();
        }

        index = activeIndex;
    }
}
