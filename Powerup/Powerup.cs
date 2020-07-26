using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour {
    [SerializeField] protected float activeTime;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            // if (activeTime == 0) {
            //     ActivatePower(other.gameObject.GetComponent<PowerupManager>());
            //     Destroy(gameObject);
            // } else {
            //     StartCoroutine(Pickup(other));
            // }

            StartCoroutine(Pickup(other));
        }
    }

    private IEnumerator Pickup(Collider2D player) {
        ActivatePower(player.gameObject.GetComponent<PowerupManager>());

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        
        yield return new WaitForSeconds(activeTime);

        Destroy(gameObject);
    }

    public abstract void ActivatePower(PowerupManager player);
}
