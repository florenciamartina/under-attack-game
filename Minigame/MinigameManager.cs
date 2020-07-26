using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MinigameManager : MonoBehaviour {

    [Header("Score UI")]
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI highscoreDisplay;
    private int score = 0;
    private int highscore = 0;

    [SerializeField] private float timeInterval = 1f;
    private float currTime = 0;

    [Header("UI")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject gameOverUI;

    [Header("Theme")]
    [SerializeField] private BGManager bGManager;

    private int numbg;

    [SerializeField] private Spawner spawner;

    [SerializeField] private int scoreInterval = 50;

    private int i = 0;

    // Start is called before the first frame update
    void Start() {
        score = 0;
        scoreDisplay.text = score.ToString();
        highscore = SaveLoad.GetHighscore();
        if (highscore != 0) {
            highscoreDisplay.text = "HI " + highscore.ToString();
        } else {
            highscoreDisplay.gameObject.SetActive(false);
        }

        numbg = bGManager.GetNumBG();
        player = GameObject.Find("Player (Minigame)");
        gameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

        if (currTime <= 0) {
            currTime = timeInterval;
            scoreDisplay.text = score++.ToString();

            i = (score % (scoreInterval * numbg)) / scoreInterval;
            bGManager.Select(i);
            spawner.Select(i);

        } else {
            currTime -= Time.deltaTime;
        }

        if (score > highscore) {
            highscore = score;
            highscoreDisplay.text = "HI " + highscore.ToString();
        }

        if (!player.activeSelf) {
            GameOver();
        }
    }

    private void GameOver() {
        if (score >= highscore) SaveLoad.SaveHighScore(highscore);
        gameOverUI.SetActive(true);
    }   
}
