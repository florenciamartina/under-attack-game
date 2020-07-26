using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    [SerializeField] private float speed = 3f;
    
    private void Update() {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x <= -11) Destroy(gameObject); 
    }
     
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            other.GetComponent<Player_Minigame>().TakeDamage();
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed) {
        this.speed = speed;
    }
}
