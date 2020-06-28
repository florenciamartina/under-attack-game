using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[System.Serializable]
public class SaveLoad: MonoBehaviour {

    private static int user;
    private static int currentSceneIndex;
    private static int sceneToContinue;
    private static int currentNutrigems;
    private static int savedNutrigems;
    // private Dictionary<string, int> powerups = new Dictionary<string, int>();
    // private Dictionary<string, int> characters = new Dictionary<string, int>();
    // private Dictionary<string, int> pathogens = new Dictionary<string, int>();

    private static string[] data = {"username", "CurrScene", "CurrNutrigems"};

    // public static SaveLoad saveManager = new SaveLoad();

    // private void OnValidate() {

    //     AddPowerups();
    // }

    // private void AddPowerups() {

    //     // 0 means powerup is not yet available
    //     powerups.Add("shield", 0);
    //     powerups.Add("NKC", 0);
    // }

    public static void SetUser(int i) {
        user = i;
    }

    // public static void AddPowerup(string name) {
    //     powerups[name] = 1;
    // }

    // private void AddCharacters(string name) {
    //     characters.Add("Whiteboi", 0);
    //     characters.Add("Oldie", 0);
    // }

    // public void AddCharacter(string name) {
    //     characters[name] = 1;
    // }    

    // public void AddPathogen(string name) {
    //     pathogens[name] = 1;
    // }

    // public void SaveCharacters(string[] characters) {
    //     foreach(string character in characters) {
    //         this.characters[character] = 1;
    //     }

    //     Save();
    // }

    // public void SavePathogens(string[] characters) {
    //     foreach(string character in characters) {
    //         pathogens[character] = 1;
    //     }

    //     Save();
    // }

    public static void Save(string powerupName) {
        // AddPowerup(powerupName);
        Save();
    }

    public static void Save() {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        currentNutrigems = PlayerStats.GetNutrigems();
        PlayerPrefs.SetInt("CurrScene" + user, currentSceneIndex);
        PlayerPrefs.SetInt("CurrNutrigems" + user, currentNutrigems);
        
        // foreach(string powerup in powerups.Keys) {
        //     PlayerPrefs.SetInt(powerup + user, powerups[powerup]);
        // }

        // foreach(string character in characters.Keys) {
        //     PlayerPrefs.SetInt(character + user, characters[character]);
        // }

        // foreach(string character in pathogens.Keys) {
        //     PlayerPrefs.SetInt(character + user, pathogens[character]);
        // }
    }

    public static void Resume() {
        Debug.Log("SL USER" + user);
        sceneToContinue = PlayerPrefs.GetInt("CurrScene" + user);
        savedNutrigems = PlayerPrefs.GetInt("CurrNutrigems" + user);
        if (sceneToContinue > 0) {
            SceneManager.LoadScene(sceneToContinue);
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    // public static int GetNutrigems() {
    //     return savedNutrigems;
    // }

    public static int GetLevel() {
        int sceneIndex = PlayerPrefs.GetInt("CurrScene" + user);
        if (sceneIndex <= 6) {
            return 1;
        } else if (sceneIndex <= 10) {
            return 2;
        } else {
            return 0;
        }
    }

    public static void ResetData(int user) {
        foreach(string playerdata in data) {
            PlayerPrefs.DeleteKey(playerdata + user);
        }
    }
}
