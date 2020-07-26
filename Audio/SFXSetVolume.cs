using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SFXSetVolume : MonoBehaviour {

    [SerializeField] private AudioMixer SFXMixer;
    [SerializeField] private GameObject SFXMuted;
    [SerializeField] private GameObject SFXUnmuted;

    public void SetVolumeLevel(float sliderValue)
    {

        SFXMixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);

        if (sliderValue <= 0.0001)
        {
            SFXMuted.SetActive(true);
            SFXUnmuted.SetActive(false);
        }
        else
        {
            SFXMuted.SetActive(false);
            SFXUnmuted.SetActive(true);
        }

    }

}
