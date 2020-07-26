using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour {

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Image button;
    [SerializeField] private Sprite muted;
    [SerializeField] private Sprite unmuted;
    [SerializeField] private float currVol = 1;
    [SerializeField] private bool isBGM;
    private string mixerName;

    private bool isMuted = false;
    private void Awake() {
        mixerName = isBGM ? "BGMVolume" : "SFXVol";
    }

    private void Start() {
        audioMixer.SetFloat(mixerName, Mathf.Log10(currVol) * 20);
    }

    public void SetVolumeLevel(float sliderValue) {

        currVol = sliderValue;
        audioMixer.SetFloat(mixerName, Mathf.Log10(currVol) * 20);

        if (sliderValue <= 0.0001) {
            button.sprite = muted;
            isMuted = true;
        } else {
            button.sprite = unmuted;
            isMuted = false;
        }
    }

    public void MuteOrUnmute() {
        if (isMuted) {
            button.sprite = unmuted;
            isMuted = false;
            audioMixer.SetFloat(mixerName, Mathf.Log10(currVol) * 20);
        } else {
            button.sprite = muted;
            isMuted = true;
            audioMixer.SetFloat(mixerName, Mathf.Log10(0.0001f) * 20);
        }
    }
}
