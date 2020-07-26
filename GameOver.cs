using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour {

    
    public void Replay() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        GameObject[] dialogueBoxes = GameObject.FindGameObjectsWithTag("Dialogue");
        foreach(GameObject dialogue in dialogueBoxes) {
            Destroy(dialogue);
        }
    }

    public void QuitGame() {
        Debug.Log("quit game");
        Time.timeScale = 1f;
        Application.Quit();
    }
}
