using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject SettingsMenuUI;

    public void OpenSettings() {
        SettingsMenuUI.SetActive(true);
    }

    public void CloseSettings() {
        SettingsMenuUI.SetActive(false);
    }

    /*
    [SerializeField] private AudioMixer SFXMixer;
    [SerializeField] private AudioMixer BGMMixer;
    [SerializeField] private GameObject BGMUnmuted;
    [SerializeField] private GameObject BGMMuted;
    [SerializeField] private GameObject SFXUnmuted;
    [SerializeField] private GameObject SFXMuted;
    [SerializeField] private GameObject SFXSlider;
    [SerializeField] private GameObject BGMSlider;
    private float currVolBGM;
    private float currVolSFX;



    private bool isBGMMuted = false;
    private bool isSFXMuted = false;

    private void Start()
    {
        if(isBGMMuted)
        {
            BGMMuted.SetActive(true);
            BGMUnmuted.SetActive(false);
        } else
        {
            BGMMuted.SetActive(false);
            BGMUnmuted.SetActive(true);
        }

        if (isSFXMuted)
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


    public void UnmuteBGM()
    {

        BGMMixer.SetFloat("BGMVolume", Mathf.Log10((float) 0.3) * 20);
        BGMMuted.SetActive(false);
        BGMUnmuted.SetActive(true);
        isBGMMuted = false;
        
    }

    public void MuteBGM()
    {
        BGMMixer.SetFloat("BGMVolume", Mathf.Log10((float) 0.001) * 20);
        BGMMuted.SetActive(true);
        BGMUnmuted.SetActive(false);
        isBGMMuted = true;
    }

    public void UnmuteSFX()
    {

        SFXMixer.SetFloat("SFXVol", Mathf.Log10((float) 0.6) * 20);
        SFXMuted.SetActive(false);
        SFXUnmuted.SetActive(true);
        isSFXMuted = false;
    }

    public void MuteSFX()
    {
        SFXMixer.SetFloat("SFXVol", Mathf.Log10((float)0.001) * 20);
        SFXMuted.SetActive(true);
        SFXUnmuted.SetActive(false);
        isSFXMuted = true;
    }
    */
}

