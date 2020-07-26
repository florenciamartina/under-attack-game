using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private TextMeshProUGUI cardDesc;
    [SerializeField] private TextMeshProUGUI price;

    private void Start() {
        icon.sprite = card.icon;
        cardName.text = card.name;
        cardDesc.text = card.desc;
        price.text = card.price.ToString();
    }

    public int GetID() {
        return card.powerupID;
    }
}
