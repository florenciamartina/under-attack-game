using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : Powerup
{
    [SerializeField] Sprite costume;
    [SerializeField] private GameObject shield;

    public override void ActivatePower() {
        costumes.SetActive(true);
        costumes.GetComponent<SpriteRenderer>().sprite = this.costume;

        powerups.SetActive(true);
        shield.SetActive(true);
        shield.GetComponent<Shield>().ResetShield();
    }
}
