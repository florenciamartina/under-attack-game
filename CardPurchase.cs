using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardPurchase: MonoBehaviour {

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI nutrigemsNumber;
    [SerializeField] private GameObject warning;
    [SerializeField] private float warningTime = 1f;
    [SerializeField] private NextButton nextButton;
    private int nutrigems;

    [Header("SFX")]
    [SerializeField] private AudioSource purchaseSound;


    // Start is called before the first frame update
    private void Start() {
        nutrigems = SaveLoad.GetNutrigems();
        nutrigemsNumber.text = nutrigems.ToString();

        warning.SetActive(false);
    }

    public void PurchasePowerup(Card powerup) {

        int price = powerup.price;

        if (price <= nutrigems) {

            if (purchaseSound != null) purchaseSound.Play();

            nutrigems -= price;
            nutrigemsNumber.text = nutrigems.ToString();
            PlayerStats.Purchase(price);
            SaveLoad.Save(powerup.powerupID);

            nextButton.NextScene();

        } else {
            Debug.Log("Not enough nutrigems");
            StartCoroutine(Warn());
        }
    }

    private IEnumerator Warn() {
        warning.SetActive(true);
        yield return new WaitForSeconds(warningTime);
        warning.SetActive(false);
    }
}
