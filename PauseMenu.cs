using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private bool isPaused;

    private void Update() {
        if (!isPaused && Input.GetButtonDown("Menu")) {
            ActivateMenu();
        } else if (isPaused && Input.GetButtonDown("Menu")) {
            DeactivateMenu();
        }
    }

    public void ActivateMenu() {
        isPaused = true;
        Time.timeScale = 0f; //to freeze the screen
        AudioListener.pause = true; //to pause the audio
        pauseMenuUI.SetActive(true); //to pop up the pause menu UI
    }

    public void DeactivateMenu() {
        isPaused = false;
        Time.timeScale = 1f; //to unfreeze the screen
        AudioListener.pause = false; //to resume the audio
        pauseMenuUI.SetActive(false);
    }

    public void BackToMenu() {
        DeactivateMenu();
        SceneManager.LoadScene(0);
    }

    public void LoadAlmanac() {
        DeactivateMenu();
        SceneManager.LoadScene("almanac");
    }
}
