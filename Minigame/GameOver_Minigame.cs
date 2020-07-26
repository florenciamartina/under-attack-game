using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameOver_Minigame : MonoBehaviour {
    [SerializeField] private GameObject yesButton;
    
    // Start is called before the first frame update
    void Start() {
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(yesButton);    
    }

    public void PlayAgain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
