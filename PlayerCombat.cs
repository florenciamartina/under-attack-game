using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    public bool canAttack = true;

    [Header("Attack")]
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask destructibleLayer;

    [SerializeField] private int damage = 20;

    [SerializeField] private float attackInterval = 0.2f;
    private float currAttackTime = 0;
    [SerializeField] private AudioSource meleeSound;
    
    [Header("Shoot")]
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float shootInterval = 0.5f;
    private float currShootTime = 0;

    [SerializeField] private AntibodyReload antibodyUI;
    [SerializeField] private AudioSource shootSound;
    
    [Header("Ground Pound")]
    private CharacterController2D controller;
    private PlayerMovement player;
    private Rigidbody2D rb;
    private bool doGroundPound = false;
    private bool groundPound = false;
    private float gravityScale;
    [SerializeField] private Transform GroundPoundPoint;
    [SerializeField] private float smashRadius = 2f;
    [SerializeField] private float stopTime = 0.3f;
    [SerializeField] private float dropForce = 50f;
    [SerializeField] private int smashDamage = 80;

    [SerializeField] private AudioSource groundPound_lift;
    [SerializeField] private AudioSource groundPound_smash;

    private void Start() {
        currAttackTime = 0;
        currShootTime = 0;
        antibodyUI.SetMaxTime(shootInterval);

        controller = GetComponent<CharacterController2D>();
        player = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        gravityScale = rb.gravityScale;
    }
    
    // Update is called once per frame
    void Update() {
        
        // CanAttack();
        if (!canAttack) return;

        // Melee Attack
        if (Input.GetButtonDown("Fire1") && currAttackTime <= 0) {
            Attack();
            currAttackTime = attackInterval;
        } else {
            currAttackTime -= Time.deltaTime;
        }

        // Shoot
        if (Input.GetButtonDown("Fire2") && currShootTime <= 0) {
            Shoot();
            currShootTime = shootInterval;
        } else {
            currShootTime -= Time.deltaTime;
        }

        // Ground Pound
        if (!doGroundPound && !controller.IsGrounded() && Input.GetButtonDown("GroundPound")) {
            doGroundPound = true;
            // groundPound_lift.Play();
        }

        antibodyUI.SetTime(shootInterval - currShootTime);
    }

    private void FixedUpdate() {
        if (doGroundPound) {
            GroundPound();
        }

        doGroundPound = false;
    }
    private void Shoot() {
        Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
        shootSound.Play();
    }

    private void Attack() {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        meleeSound.Play();
        foreach (Collider2D enemy in enemies) {
            if (!enemy.isTrigger) {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
            }
        }

        Collider2D[] destructibles = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, destructibleLayer);
        foreach(Collider2D destructible in destructibles) {
            destructible.GetComponent<DestructibleTile>().TakeDamage(damage);
        }
    }

    private void GroundPound() {
        groundPound = true;

        player.canMove = false;

        StopInAir();
        StartCoroutine("Smash");
        
        player.canMove = true;
        
        // Reset gravity scale
        rb.gravityScale = gravityScale;
    }

    private void StopInAir() {
        // Stop
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0; 

        // Halt in air
        rb.gravityScale = 0;
    }

    private IEnumerator Smash() {
        yield return new WaitForSeconds(stopTime);

        // Push downwards
        rb.AddForce(Vector2.down  * dropForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // If the collided object is below the player
        if (groundPound && other.contacts[0].normal.y > 0.5) {
            CompleteGroundPound();
        }
    }

    private void CompleteGroundPound() {
        // groundPound_smash.Play();

        // Destroy destructible tiles
        Collider2D[] destructibles = Physics2D.OverlapCircleAll(GroundPoundPoint.position, smashRadius, destructibleLayer);
        foreach(Collider2D destructible in destructibles) {
            Destroy(destructible.gameObject);
        }

        // Damage surrounding enemies
        Collider2D[] enemies = Physics2D.OverlapCircleAll(GroundPoundPoint.position, smashRadius, enemyLayer);
        foreach(Collider2D enemy in enemies) {
            enemy.GetComponent<Enemy>().TakeDamage(smashDamage);
        }

        groundPound = false;
    }

    private void OnDrawGizmos() {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

        if (GroundPoundPoint == null) return;

        Gizmos.DrawWireSphere(GroundPoundPoint.position, smashRadius);
    }
}
