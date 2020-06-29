using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    [SerializeField] private GameObject newGame;
    [SerializeField] private GameObject resumeGame;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject almanac;
    [SerializeField] private GameObject inputWindow;
    [SerializeField] private Transform usersParent;
    [SerializeField] private TextMeshProUGUI[] users;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private AudioSource buttonSound;

    private static int user;
    private bool isNewGame = false;

    private void Awake() {
        for (int i = 0; i < users.Length; i++) {
            users[i].text = PlayerPrefs.GetString("username" + (i + 1), "User " + (i + 1)); 
        }
    }
    private void Start() {
        newGame.SetActive(true);
        resumeGame.SetActive(true);
        playButton.SetActive(false);
        almanac.SetActive(false);
        inputWindow.SetActive(false);
        usersParent.gameObject.SetActive(false);    
    }

    private void OnValidate() {
        users = usersParent.GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void SetNewGame(bool isNew) {
        isNewGame = isNew;
    }

    public void CloseNewGame()
    {
        usersParent.gameObject.SetActive(false);
        newGame.SetActive(true);
        resumeGame.SetActive(true);
    }

    public void ViewUsers() {
        newGame.SetActive(false);
        resumeGame.SetActive(false);

        usersParent.gameObject.SetActive(true);
    }

    public void SelectUser() {
        if (isNewGame) {
            inputWindow.SetActive(true);
        } else {
            PlayOrAlmanac();
        }
    }

    public void SetUser(int i) {
        user = i;
        SaveLoad.SetUser(i);
    }

    public void PlayOrAlmanac() {
        usersParent.gameObject.SetActive(false);
        playButton.SetActive(true);
        almanac.SetActive(true);
    }

    public void NewGame() {
        buttonSound.Play();
        SaveLoad.ResetData(user);
        PlayerPrefs.SetString("username" + user, nameInput.text);
        users[user - 1].text = nameInput.text;
        StartCoroutine(LoadNewScene());
    }

    private IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(0.2f);
        PlayIntro();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void PlayIntro() {
        SceneManager.LoadScene("intro");
    }

    public void Resume() {
        Debug.Log("MM USER" + user);
        // SaveLoad.saveManager.Resume();
        SaveLoad.Resume();
    }

    public void Almanac() {
        SceneManager.LoadScene("almanac");
    }

    public static int GetUser() {
        return user;
    }

    public void Quit()
    {
        Debug.Log("quit game");
        Application.Quit();
    }

}
