using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardPanel : MonoBehaviour
{
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI characterType;
    public TextMeshProUGUI characterDesc;
    public GameObject character;

    public void Show() {
        gameObject.SetActive(true);
    }
}
