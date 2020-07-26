using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    private Rigidbody2D rb;

    [SerializeField] private int damage = 5;

    [SerializeField] private bool ignoreEnemies = true;

    // Start is called before the first frame update
    protected virtual void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }
    
    protected virtual void OnCollisionEnter2D(Collision2D other) {
        if ((ignoreEnemies && other.gameObject.tag == "Enemy") || other.gameObject.tag == "Collectible") return;
        Destroy(gameObject);
    }

    private void OnBecameInvisible() {
        enabled = false;
        Destroy(gameObject);
    }

    public int getDamage() {
        return damage;
    }

    public void SetSpeed(int speed) {
        this.speed = speed;
    }
}
