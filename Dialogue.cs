using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour {
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI nameTextDisplay;
    [SerializeField] private TextMeshProUGUI typeTextDisplay;
    [SerializeField] private TextMeshProUGUI dialogueTextDisplay;

    [SerializeField] private GameObject continueButton;

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private string[] names;
    [SerializeField] private string[] types;

    [TextArea(3, 10)] 
    [SerializeField] private string[] sentences;
    
    private int index = 0;
    private int nextScene;

    [SerializeField] private float typingSpeed = .02f;
    [Header("Sound FX")]
    [SerializeField] private AudioSource bipSound;
    [SerializeField] private AudioSource contSound;

    private GameObject player;
    private PlayerMovement moveScript;
    private PlayerCombat combatScript;

    // Start is called before the first frame update
    void Start() {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(Type());
        
        player = GameObject.Find("Player");
        if (player != null) {
            Debug.Log("Player available");
            moveScript = player.GetComponent<PlayerMovement>();
            combatScript = player.GetComponent<PlayerCombat>();
            moveScript.canMove = false;
            combatScript.canAttack = false;
        }   
    }

    public void ContinueScene() {
        Debug.Log("next scene");
        SceneManager.LoadScene(nextScene + 2);
    }

    private void Update() {
        if (dialogueTextDisplay.text == sentences[index]) {
            continueButton.SetActive(true);
        }

        // to skip the typing part and show the text at once.
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (dialogueTextDisplay.text == sentences[index].ToString()) {
                StopAllCoroutines();
                NextSentence();
                return;
            }

            StopAllCoroutines();
            dialogueTextDisplay.text = sentences[index].ToString();
        }

        // if (dialogueTextDisplay.text == sentences[index].ToString()) {
        //     if (Input.GetKeyDown(KeyCode.Space)) {
        //         StopAllCoroutines();
        //         NextSentence();
        //     }
        // }
    }

    IEnumerator Type() {

        if (characterImage != null) characterImage.sprite = sprites[index];
        if (nameTextDisplay != null) nameTextDisplay.text = names[index];
        if (typeTextDisplay != null) typeTextDisplay.text = types[index];

        dialogueTextDisplay.text = "";

        foreach(char letter in sentences[index].ToCharArray()) {
            bipSound.Play();
            dialogueTextDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence() {

        continueButton.SetActive(false);
        contSound.Play();
        dialogueTextDisplay.text = "";

        if (index < sentences.Length - 1) {
            index++;
            StopAllCoroutines();
            StartCoroutine(Type());

        } else {
            if (nameTextDisplay != null) {
                nameTextDisplay.text = "";
            }

            gameObject.SetActive(false);
            
            if (player != null) {
                combatScript.canAttack = true;
                moveScript.canMove = true;
            }

            Destroy(this.gameObject);
        }
  
    }
}
