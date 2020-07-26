using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide2 : MonoBehaviour {

    [SerializeField] private Dialogue[] dialogues;
    [SerializeField] private GameObject player;
    private int i = 0;

    private void OnTriggerEnter2D(Collider2D other) {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (i < dialogues.Length - 1) {
            i++;
            dialogues[i].gameObject.SetActive(true);
        }
    }
}
