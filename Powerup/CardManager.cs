using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {
    [SerializeField] private Transform cardParent;
    private CardDisplay[] cards;

    private int cardID;

    private void Awake() {
        cards = cardParent.GetComponentsInChildren<CardDisplay>();

        foreach(CardDisplay card in cards) {
            if (SaveLoad.GetPowerup(card.GetID()) == 1) {
                card.gameObject.SetActive(false);
            } else {
                card.gameObject.SetActive(true);
            }
        }
    }
}
