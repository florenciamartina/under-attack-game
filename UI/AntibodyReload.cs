using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntibodyReload : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Awake() {
        slider = GetComponent<Slider>();
    }
    
    public void SetMaxTime(float maxTime) {
        slider.maxValue = maxTime;
        slider.value = maxTime;
    }
    public void SetTime(float time) {
        slider.value = time;
    }
}
