using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [Header("Powerups")]
    [SerializeField] private GameObject[] powerups;
    // Order: shield, nkc

    // Start is called before the first frame update
    void Start() {

        for (int i = 0; i < powerups.Length; i++) {
            if (powerups[i] != null) {
                powerups[i].SetActive(isPowerupActive(i));
            }
        }
        
    }

    private bool isPowerupActive(int powerupID) {
        return SaveLoad.GetPowerup(powerupID) == 1;
    }
}
