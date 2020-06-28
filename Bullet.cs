using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] private float speed = 20f;
    private Rigidbody2D rb;

    [SerializeField] private int damage = 40;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }
    // private void OnTriggerEnter2D(Collider2D other) {
        
    //     if (other.tag == "Collectible" || other.tag == "Player") {
    //         return;
    //     }

    //     if (other.tag == "Enemy") {
    //         Enemy enemy = other.GetComponent<Enemy>();
    //         enemy.TakeDamage(damage);
    //     }

    //     Destroy(gameObject);
    // }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Collectible") return;

        if (other.gameObject.tag == "Enemy") {

            if (other.gameObject.GetComponent<Bullet_Enemy>() != null) {
                Destroy(other.gameObject);
                Destroy(this.gameObject);
                return;
            }
            
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        } else if (other.gameObject.CompareTag("Destructible")) {
            other.gameObject.GetComponent<DestructibleTile>().TakeDamage(damage);
        }

        Destroy(gameObject);    
    }

    private void OnBecameInvisible() {
        enabled = false;
        Destroy(gameObject);
    }

}
