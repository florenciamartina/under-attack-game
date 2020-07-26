using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NKCPowerup : Powerup {
    public override void ActivatePower(PowerupManager player) {
        StartCoroutine(player.ActivateNKC(activeTime));
    }
}
