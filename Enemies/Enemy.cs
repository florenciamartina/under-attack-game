using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int maxHealth = 100;
    private int health;
    [SerializeField] private float speed = 5f;
    [SerializeField] protected int damage = 20;
    [SerializeField] private float dazedTime = .6f;
    private float currDazedTime = 0f;

    [Header("Boundaries")]
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [Header("Sound FX")]
    [SerializeField] private AudioSource enemyDead;

    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D coll;

    protected bool facingLeft = true;

    protected void Start() {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        health = maxHealth;

        if (healthBar != null) {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.gameObject.SetActive(false);
        }
    }

    protected void Update() {

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

        if (facingLeft) {
            if (transform.position.x > leftCap) {

                if (transform.localScale.x < 0) {
                    transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y);
                }

                rb.velocity = new Vector2(-speed, 0);
            } else {
                facingLeft = false;
            }
        } else {
            if (transform.position.x < rightCap) {
                if (transform.localScale.x > 0) {
                    transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y);
                }

                rb.velocity = new Vector2(speed, 0);
            } else {
                facingLeft = true;
            }
        }
    }

    public void TakeDamage(int damage) {
        animator.SetBool("Hurt", true);

        currDazedTime = dazedTime;

        health -= damage;

        if (health <= 0) {
            //enemyDead.Play();
            Debug.Log("dead");
            animator.SetTrigger("Dead");
        }
    }

    void Death() {
        //enemyDead.Play();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        // Move a few space backwards if got hit by bullet.
        if (other.gameObject.tag == "Bullet") {
			if (other.gameObject.transform.position.x > gameObject.transform.position.x) {
				rb.AddForce(new Vector2(-100f, 0f));
			} else {
				rb.AddForce(new Vector2(100f, 0f));
			}
        }
    }

    public int getDamage() {
        return damage;
    }
}
