using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using UnityEngine.UIElements;

[System.Serializable]
public class SaveLoad: MonoBehaviour {

    private static int user;
    
    private static int prevSceneIndex;
    private static int currentSceneIndex;
    private static int sceneToContinue;
    private static int currentNutrigems;
    private static int savedNutrigems;

    private static string[] powerups = {"Shield", "NKC"};
    private static string[] data = {"username", "PrevScene", "CurrScene", "CurrNutrigems", "Highscore"};

    public static int minLevel1 = 3;
    public static int maxLevel1 = minLevel1 + 5;
    public static int minLevel2 = maxLevel1 + 1;
    public static int maxLevel2 = minLevel2 + 6;
    public static int minLevel3 = maxLevel2 + 1;
    public static int maxLevel3 = minLevel3 + 2;

    public static void SetUser(int i) {
        user = i;
    }

    public static void Save(int powerupID) {
        PlayerPrefs.SetInt(powerups[powerupID] + user, 1);
        Save();
    }

    public static void Save() {
        prevSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentSceneIndex = prevSceneIndex + 1;
        currentNutrigems = PlayerStats.GetNutrigems();
        PlayerPrefs.SetInt("PrevScene" + user, prevSceneIndex);
        PlayerPrefs.SetInt("CurrScene" + user, currentSceneIndex);
        PlayerPrefs.SetInt("CurrNutrigems" + user, PlayerPrefs.GetInt("CurrNutrigems" + user) + PlayerStats.GetNutrigems());
    }

    public static void Dead() {
        PlayerPrefs.SetInt("PrevScene" + user, PlayerPrefs.GetInt("CurrScene" + user));
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

    public static int GetLevel() {
        int sceneIndex = PlayerPrefs.GetInt("CurrScene" + user, SceneManager.GetActiveScene().buildIndex);

        if (sceneIndex < minLevel1) {
            return 0;
        } else if (sceneIndex <= maxLevel1) {
            return 1;
        } else if (sceneIndex <= maxLevel2) {
            return 2;
        } else {
            return 3;
        }
    }

    public static int GetSceneIndex() {
        return PlayerPrefs.GetInt("CurrScene" + user);
    }

    public static int GetNutrigems() {
        return PlayerPrefs.GetInt("CurrNutrigems" + user);
    }

    public static int GetPowerup(int powerupID) {
        return PlayerPrefs.GetInt(powerups[powerupID] + user, 0);
    }

    public static int[] GetSuperpowers() {
        int[] superpowers = new int[powerups.Length];
        for (int i = 0; i < superpowers.Length; i++) {
            superpowers[i] = GetPowerup(i);
        }

        return superpowers;
    }

    public static void ResetData(int user) {
        foreach(string playerdata in data) {
            PlayerPrefs.DeleteKey(playerdata + user);
        }

        foreach(string powerup in powerups) {
            PlayerPrefs.DeleteKey(powerup + user);
        }
    }
    public static bool IsFirstTime() {
        // Debug.Log(user);
        // Debug.Log(PlayerPrefs.GetInt("PrevScene" + user, -1));
        // Debug.Log(PlayerPrefs.GetInt("CurrScene" + user, -2));
        return PlayerPrefs.GetInt("PrevScene" + user, -1) != PlayerPrefs.GetInt("CurrScene" + user, -2);
    }

    public static int GetHighscore() {
        return PlayerPrefs.GetInt("Highscore" + user, 0);
    }

    public static void SaveHighScore(int highscore) {
        PlayerPrefs.SetInt("Highscore" + user, highscore);
    }
}
