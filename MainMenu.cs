using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour {
    [SerializeField] private GameObject firstMenu;
    [SerializeField] private GameObject secondMenu;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject playOrAlmanac;
    [SerializeField] private GameObject playButton;

    [Header("SFX")]
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioSource backSound;

    [Header("Input Window")]
    [SerializeField] private GameObject inputWindow;
    [SerializeField] private GameObject confirmButton;
    [SerializeField] private GameObject usersWindow;
    [SerializeField] private TMP_InputField nameInput;

    [SerializeField] private TextMeshProUGUI warningText;

    [Header("Select User")]
    [SerializeField] private Transform usersParent;
    [SerializeField] private TextMeshProUGUI[] users;

    [Header("Delete User")]
    [SerializeField] private GameObject deleteWindow;
    [SerializeField] private GameObject confirmDelete;

    private GameObject prevScene;
    private static int user;
    private bool isNewGame = false;

    private int deleteUser;

    private void Awake() {
        for (int i = 0; i < users.Length; i++) {
            users[i].text = PlayerPrefs.GetString("username" + (i + 1), "NEW GAME"); 
        }
    }
    private void Start() {
        firstMenu.SetActive(true);
        secondMenu.SetActive(false);
        inputWindow.SetActive(false);
        deleteWindow.SetActive(false);
        usersWindow.gameObject.SetActive(false);
        settingsUI.SetActive(false);
    }

    private void Update() {
        if (inputWindow.activeSelf) {
            if (Input.GetAxis("Vertical") < 0) {
                EventSystem.current.SetSelectedGameObject(confirmButton);
            }
        }
    }

    private void OnValidate() {
        users = usersParent.GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void SetNewGame(bool isNew) {
        isNewGame = isNew;
    }

    public void Back() {
        backSound.Play();

        playOrAlmanac.SetActive(false);
        inputWindow.SetActive(false);
        deleteWindow.SetActive(false);
        warningText.gameObject.SetActive(false);
        settingsUI.SetActive(false);

        if (prevScene == firstMenu) {
            secondMenu.SetActive(false);
            usersWindow.SetActive(false);
            firstMenu.SetActive(true);

            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);

        } else if (prevScene == usersWindow) {
            prevScene = firstMenu;

            firstMenu.SetActive(false);
            secondMenu.SetActive(true);
            usersWindow.SetActive(true);

            EventSystem.current.SetSelectedGameObject(users[0].gameObject);
        }

    }

    public void ViewUsers() {
        buttonSound.Play();
        firstMenu.SetActive(false);
        secondMenu.SetActive(true);
        playOrAlmanac.SetActive(false);
        usersWindow.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(users[0].gameObject);
        
        prevScene = firstMenu;
    }


    public void PlayOrAlmanac() {
        buttonSound.Play();
        usersWindow.gameObject.SetActive(false);
        playOrAlmanac.SetActive(true);
        EventSystem.current.SetSelectedGameObject(playButton);
    }

    public void NewGame() {
        buttonSound.Play();
        SaveLoad.ResetData(user);
        if (!nameInput.text.Equals("NEW GAME") && !nameInput.text.Equals("")) {
            PlayerPrefs.SetString("username" + user, nameInput.text);
            users[user - 1].text = nameInput.text;
            StartCoroutine(LoadNewScene());
        } else {
            StartCoroutine(WarningText());
        }
    }

    private IEnumerator WarningText() {
        warningText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        warningText.gameObject.SetActive(false);
    }

    private IEnumerator LoadNewScene() {
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
        buttonSound.Play();
        SceneManager.LoadScene("almanac");
    }

    public void Minigame() {
        buttonSound.Play();
        SceneManager.LoadScene("minigame");
    }

    public static int GetUser() {
        return user;
    }

    public void Quit() {
        Debug.Log("quit game");
        Application.Quit();
    }

    public void SelectUser(int i) {
        buttonSound.Play();
        SetUser(i);

        prevScene = usersWindow;

        if (IsNewGame()) {
            inputWindow.SetActive(true);
            nameInput.Select();
            usersWindow.gameObject.SetActive(false);
        } else {
            PlayOrAlmanac();
        }
    }

    private void SetUser(int i) {
        user = i;
        SaveLoad.SetUser(i);
    }

    private bool IsNewGame() {
        return users[user-1].text.Equals("NEW GAME");
    }

    public void Credits() {
        SceneManager.LoadScene("creditroll");
    }

    public void ShowDeleteWindow(int toDeleteUser) {
        deleteWindow.SetActive(true);
        EventSystem.current.SetSelectedGameObject(confirmDelete);
        deleteUser = toDeleteUser;
    }

    public void DeleteAllPlayerData() {
        //type here
        Debug.Log("deleted all data");
        SaveLoad.ResetData(deleteUser);
        users[deleteUser - 1].text = "NEW GAME";
        deleteWindow.SetActive(false);
        EventSystem.current.SetSelectedGameObject(users[0].gameObject);
    }

    public void ShowSettings() {
        settingsUI.SetActive(true);
        prevScene = firstMenu;
    }
}
