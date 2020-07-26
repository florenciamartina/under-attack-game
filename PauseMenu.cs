using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour {
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private bool isPaused;

    private bool isSettingsOpen = false;

    private void Start() {
        pauseMenuUI.SetActive(false);
        settingsUI.SetActive(false);
    }

    private void Update() {
        if (!isPaused && Input.GetButtonDown("Menu")) {
            ActivateMenu();
        } else if (isPaused && Input.GetButtonDown("Menu")) {
            if (isSettingsOpen) {
                CloseSettings();
            } else {
                DeactivateMenu();
            }
        }
    }

    public void ActivateMenu() {
        isPaused = true;
        Time.timeScale = 0f; //to freeze the screen
        //AudioListener.pause = true; //to pause the audio
        
        settingsUI.SetActive(false);
        pauseMenuUI.SetActive(true); //to pop up the pause menu UI
        EventSystem.current.SetSelectedGameObject(resumeButton);
    }

    public void DeactivateMenu() {
        isPaused = false;
        Time.timeScale = 1f; //to unfreeze the screen
        //AudioListener.pause = false; //to resume the audio
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

    public void OpenSettings() {
        isSettingsOpen = true;
        settingsUI.SetActive(true);
    }

    public void CloseSettings() {
        isSettingsOpen = false;
        settingsUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(settingsButton);
    }
}
