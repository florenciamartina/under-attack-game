using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    [SerializeField] protected GameObject costumes;
    [SerializeField] protected GameObject powerups;
    [SerializeField] private float time;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if (time == 0) {
                ActivatePower();
                Destroy(gameObject);
            } else {
                StartCoroutine(Pickup(other));
            }
        }
    }

    private IEnumerator Pickup(Collider2D player) {
        ActivatePower();

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        
        yield return new WaitForSeconds(time);

        costumes.SetActive(false);
        powerups.SetActive(false);

        Destroy(gameObject);
    }

    public abstract void ActivatePower();
}
