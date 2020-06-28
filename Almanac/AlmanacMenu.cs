using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlmanacMenu : MonoBehaviour {
    public void LoadMenu() {
        SceneManager.LoadScene(0);
    }

    public void LoadPathogenAlmanac() {
        SceneManager.LoadScene("almanac_pathogen");
    }

    public void LoadWBCAlmanac() {
        SceneManager.LoadScene("almanac");
    }

    public void ResumeGame() {
        SaveLoad.Resume();
    }
    
}