using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatsFull : PlayerStats {

    // Start is called before the first frame update
    private new void Start() {
        nutrigems = PlayerPrefs.GetInt("CurrNutrigems" + MainMenu.GetUser());
        nutrigemsNumber.text = nutrigems.ToString();
    }

    public void purchasePowerup(int price, string powerupName) {

        if (price <= nutrigems) {
            nutrigems -= price;
            nutrigemsNumber.text = nutrigems.ToString();
            // SaveLoad.saveManager.Save(powerupName);
            SaveLoad.Save(powerupName);
        } else {
            Debug.Log("Not enough nutrigems");
        }
    }
}
