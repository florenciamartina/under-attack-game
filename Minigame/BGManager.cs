using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGManager : MonoBehaviour {
    [SerializeField] private GameObject[] backgrounds;
    private int activeIndex = 0;

    public int GetNumBG() {
        return backgrounds.Length;
    }

    public void Select(int i) {
        if (i >= backgrounds.Length) return;
        if (i == activeIndex) return;

        foreach(GameObject bg in backgrounds) {
            bg.SetActive(false);
        }

        backgrounds[i].SetActive(true);
        activeIndex = i;
    }
}
