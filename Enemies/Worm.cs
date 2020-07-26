using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour {

    [SerializeField] private float attackInterval = 5f;
    [SerializeField] private AnimationClip attackAnimation;
    private float attackTime;
    [SerializeField] private int damage = 30;
    private float currTime = 0;
    private Animator animator;
    private Collider2D coll;
    private bool playerInRange;
    private GameObject target;
    private bool isAttack;
    private bool targetAttacked;

    // Start is called before the first frame update
    private void Start() {
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        coll.isTrigger = true;
        currTime = Random.Range(0f, 0.5f);
        attackTime = attackAnimation.length;
    }

    // Update is called once per frame
    private void Update() {
        if (currTime <= 0) {
            StartCoroutine("Attack");
            currTime = attackInterval;
        } else {
            currTime -= Time.deltaTime;
        }
    }

    private IEnumerator Attack() {
        isAttack = true;
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.3f);

        coll.isTrigger = false;

        yield return new WaitForSeconds(attackTime - 0.3f);

        coll.isTrigger = true;
        animator.ResetTrigger("Attack");
        isAttack = false;
        targetAttacked = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }
    
    private void OnTriggerStay2D(Collider2D other) {
        if (isAttack && other.gameObject.CompareTag("Player") && !targetAttacked) {
            targetAttacked = true;
            target = other.gameObject;
            target.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }
}
