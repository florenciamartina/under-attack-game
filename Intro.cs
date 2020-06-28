using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private RawImage video;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Button continueButton;

    private float time = 0f;

    private void Update() {
        if (dialogue == null) {
            video.gameObject.SetActive(true);
            videoPlayer.gameObject.SetActive(true);
            time += Time.deltaTime;
        }

        if (time >= videoPlayer.length) {
            continueButton.gameObject.SetActive(true);
        }
    }
}
