using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour {
    [SerializeField] private Transform parentBG;
    [SerializeField] private RepeatBG[] backgrounds;

    [SerializeField] private float baseSpeed = 3f;

    private float currSpeed;

    private void OnValidate() {
        backgrounds = parentBG.GetComponentsInChildren<RepeatBG>();
    }

    private void Start() {
        baseSpeed = 3f;

        currSpeed = 5f;
        foreach(RepeatBG bg in backgrounds) {
            bg.SetSpeed(currSpeed);
            currSpeed -= 2f / backgrounds.Length;
        }
    }
}
