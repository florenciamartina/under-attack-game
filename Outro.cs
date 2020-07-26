using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Outro : MonoBehaviour {

    [SerializeField] private Image black;
    [SerializeField] private Image blackCredits;
    [SerializeField] private float fadeTime = 10f;
    [SerializeField] private RawImage rawImage;
    [SerializeField] private VideoPlayer[] videos;
    [SerializeField] private Dialogue[] dialogues;
    [SerializeField] private GameObject creditRoll;

    private bool isCredits = false;

    // Start is called before the first frame update
    void Start() {
        black.gameObject.SetActive(false);
        rawImage.gameObject.SetActive(false);
        creditRoll.gameObject.SetActive(false);

        foreach(VideoPlayer video in videos) {
            video.gameObject.SetActive(false);
        }
        
        foreach(Dialogue dialogue in dialogues) {
            dialogue.gameObject.SetActive(false);
        }

        dialogues[0].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update() {

        if (isCredits && !creditRoll.gameObject.activeSelf) {
            SceneManager.LoadScene(0);
        }

        if (dialogues[3] == null) {

            black.gameObject.SetActive(true);
            StartCoroutine(FadeToBlack());        

        } else if (dialogues[2] == null) {
            
            if (videos[2] == null) return;
            if (!videos[2].gameObject.activeSelf) StartCoroutine(PlayVideo(2));


        } else if (dialogues[1] == null) {

            if (videos[1] == null) return;
            if (!videos[1].gameObject.activeSelf) StartCoroutine(PlayVideo(1));

        } else if (dialogues[0] == null) {

            if (videos[0] == null) return; 

            if (!rawImage.gameObject.activeSelf) rawImage.gameObject.SetActive(true);

            if (!videos[0].gameObject.activeSelf) StartCoroutine(PlayVideo(0));
        }
    }


    private IEnumerator PlayVideo(int i) {
        videos[i].gameObject.SetActive(true);
        videos[i].Play();

        yield return new WaitForSeconds((float) videos[i].length + 1f);

        Destroy(videos[i].gameObject);
        if (!dialogues[i + 1].gameObject.activeSelf) dialogues[i + 1].gameObject.SetActive(true);
    }

    private IEnumerator FadeToBlack() {
        for (float i = 0; i <= fadeTime; i += Time.deltaTime) {
            black.color = new Color(0, 0, 0, i / (fadeTime / 2));
            yield return null;
        }

        creditRoll.gameObject.SetActive(true);

        for (float i = fadeTime; i >= 0; i -= Time.deltaTime) {
            blackCredits.color = new Color(0, 0, 0, i / (fadeTime / 2));
            yield return null;
        }

        isCredits = true;
    }
}
