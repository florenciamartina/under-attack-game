using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [Header("Stats")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int maxHealth = 100;
    private int health;
    [SerializeField] private int bulletType;
    [SerializeField] private float speed = 5f;
    [SerializeField] protected int damage = 20;
    [SerializeField] private float dazedTime = 1f;
    private float currDazedTime = 0f;

    [Header("Boundaries")]
    [SerializeField] protected float leftCap;
    [SerializeField] protected float rightCap;

    [Header("Sound FX")]
    [SerializeField] private AudioSource enemyDead;

    [SerializeField] protected ParticleSystem deadEffect;

    protected Animator animator;
    protected Rigidbody2D rb;
    private Collider2D coll;
    protected bool facingLeft = true;

    protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        health = maxHealth;

        if (healthBar != null) {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.gameObject.SetActive(false);
        }
    }

    protected virtual void Update() {
        Move();

        if (healthBar != null) {
            if (health < maxHealth) {
                healthBar.gameObject.SetActive(true);
            }

            healthBar.SetHealth(health);
        }
    }

    private void FixedUpdate() {
        animator.SetBool("Hurt", false);
    }

    protected virtual void Move() {

        if (currDazedTime > 0) {
            currDazedTime -= Time.deltaTime;
            return;
        }

        // if (facingLeft) {
        //     if (transform.position.x > leftCap) {

        //         if (transform.localScale.x < 0) {
        //             transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y);
        //         }

        //         rb.velocity = new Vector2(-speed, 0);

        //     } else {
        //         facingLeft = false;
        //     }
        // } else {
        //     if (transform.position.x < rightCap) {

        //         if (transform.localScale.x > 0) {
        //             transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y);
        //         }

        //         rb.velocity = new Vector2(speed, 0);
        //     } else {
        //         facingLeft = true;
        //     }
        // }

        if (transform.position.x < leftCap && facingLeft || 
            transform.position.x > rightCap && !facingLeft) Flip();

        rb.velocity = facingLeft 
            ? new Vector2(-speed, 0)
            : new Vector2(speed, 0);
    }

    public virtual void TakeDamage(int damage) {
        animator.SetBool("Hurt", true);

        currDazedTime = dazedTime;

        health -= damage;

        if (health <= 0) {
            //enemyDead.Play();
            Debug.Log("dead");
            animator.SetTrigger("Dead");
        }
    }

    public virtual void Death() {
        //enemyDead.Play();
        if (deadEffect != null) Instantiate(deadEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        if (other.gameObject.CompareTag("Destructible")) return;
        
        // Move a few space backwards if got hit by bullet.
        if (other.gameObject.tag == "Bullet") {
			if (other.gameObject.transform.position.x > gameObject.transform.position.x) {
				rb.AddForce(new Vector2(-100f, 0f));
			} else {
				rb.AddForce(new Vector2(100f, 0f));
			}
        } else {
            Flip();
        }
    }

    public int getDamage() {
        return damage;
    }

    public int GetBulletType() {
        return bulletType;
    }

    protected void Flip() {
        facingLeft = !facingLeft;
        transform.Rotate(0f, 180f, 0f);
    }
}
