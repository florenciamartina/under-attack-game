using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    [SerializeField] private int shieldMaxStrength;
    private int shieldStrength;

    [SerializeField] private HealthBar shieldBar;
    [SerializeField] private GameObject costume;

    // Start is called before the first frame update
    void Start()
    {
        ResetShield(); 
    }

    public void TakeDamage(int damage) {
        shieldStrength -= damage;
        shieldBar.SetHealth(shieldStrength);

        if (shieldStrength <= 0) {
            gameObject.SetActive(false);
            costume.SetActive(false);
        }   
    }

    public void ResetShield() {
        shieldBar.SetMaxHealth(shieldMaxStrength);
        shieldStrength = shieldMaxStrength;
    }
}
