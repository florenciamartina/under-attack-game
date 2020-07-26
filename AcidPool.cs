using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPool : MonoBehaviour {

    [SerializeField] private int dps = 5;
    [SerializeField] private AudioSource acidSound;
    [SerializeField] private AudioSource acidBubbleSound;
    private float currTime = 0;
    private bool hasPlayed = false;

    private void Start() {
        if (acidBubbleSound != null) acidBubbleSound.Play();
    }

    private void Update() {
        currTime -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (currTime <= 0 && other.gameObject.CompareTag("Player")) {
            currTime = 1f;
            other.GetComponent<PlayerStats>().TakeDamage(dps);
            acidSound.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            acidSound.Stop();
        }
    }
}
