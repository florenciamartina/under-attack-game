﻿using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class YellowBacteria: Enemy {

    [Header("Player Detection")]
    [SerializeField] private Transform raycast;
    [SerializeField] private LayerMask raycastMask;
    [SerializeField] private float raycastLength;
    [SerializeField] private float attackDistance;  // minimum distance for attack
    [SerializeField] private float moveSpeed;

    [Header("Attack")]
    [SerializeField] private float timer;           // timer for cooldown between attack
    
    private RaycastHit2D hit;
    private GameObject target;
    private Animator anim;
    private float distance;         // Distance between the enemy and the player
    private bool attackMode;
    private bool inRange;           // Check if the player is in range
    private bool cooling = false;   // Check if the enemy is cooling after the attack
    private float initTimer;

    private void Awake() {
        initTimer = timer;                   
        anim = GetComponent<Animator>();
    }

    protected override void Move() {
        if (inRange) {
            Debug.Log("Player in range");
            hit = Physics2D.Raycast(raycast.position, Vector2.left, raycastLength, raycastMask);

            if (hit) {
                if (!facingLeft) {
                    Flip();
                }
                EnemyBehaviour();
            } else {
                hit = Physics2D.Raycast(raycast.position, Vector2.right, raycastLength, raycastMask);

                if (hit) {
                    if (facingLeft) {
                        Flip();
                    }

                    EnemyBehaviour();
                }
            }
        } else {
            base.Move();
        }

        RaycastDebugger();
    }

    

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            target = collision.gameObject;
            inRange = true;
            Debug.Log("Player within trigger range");
        } else {
            inRange = false;
        }

        if (!inRange) {
            StopAttack();
        }

        //When player is detected
        // if (hit) {
        //     EnemyBehaviour();
        // } else {
        //     Debug.Log("hit null");
        //     inRange = false;
        // }

        // if(!inRange) {
        //     anim.SetBool("canAttack", false);
        //     //StopAttack();
        // }
    }

    void EnemyBehaviour() {
        distance = Vector2.Distance(transform.position, target.transform.position);
        
        if(distance > attackDistance) {
            Debug.Log("Player not within attack range");
            MoveTowardsPlayer();
            StopAttack();
        } else if (distance <= attackDistance && !cooling) {
            Debug.Log("Player within attack range");
            Attack();
        }

        if(cooling) {
            Cooldown();
            anim.SetBool("canAttack", false);
        }
    }

    private void MoveTowardsPlayer() {
        //Move();
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("yellowAttack")) {
            Debug.Log("Enemy moving towards player");
            //Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 
                                                     moveSpeed * Time.deltaTime);
        }
    }

    private void Attack() {
        timer = initTimer;
        attackMode = true;
        anim.SetBool("canAttack", true);

        target.GetComponent<PlayerStats>().TakeDamage(damage);
    }

    void StopAttack() {
        cooling = false;
        attackMode = false;
        anim.SetBool("canAttack", false);
    }

    void RaycastDebugger() {
        Color color = distance > attackDistance ? Color.red : Color.green;
        Debug.DrawRay(raycast.position, Vector2.left * raycastLength, color);
        Debug.DrawRay(raycast.position, Vector2.right * raycastLength, color);
    }

    public void TriggerCooling() {
        cooling = true;
    }

    void Cooldown() {
        timer -= Time.deltaTime;
        if(timer <= 0 && cooling && attackMode) {
            cooling = false;
            timer = initTimer;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }

    private void Flip() {
		// Switch the way the player is labelled as facing.
        facingLeft = !facingLeft;
		transform.Rotate(0f, 180f, 0f);
	}

}
