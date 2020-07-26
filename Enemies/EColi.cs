using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EColi : Enemy {
    // [SerializeField] private Tilemap platform;

    [Header("Explode")]

    [SerializeField] private AudioSource countdown;
    [SerializeField] private AudioSource exploded;
    [SerializeField] private float explosionTime = 3f;
    [SerializeField] private GameObject explosionEffect;

    [SerializeField] private float outerBlastRadius = 5f;
    [SerializeField] private float innerBlastRadius = 3f;
    [SerializeField] private int explosionForce = 20;

    private Collider2D[] innerHits;

    private List<Collider2D> iHits;
    private Collider2D[] outerHits;

    private bool isExplode = false;

    protected override void Start() {
        base.Start();
        facingLeft = true;
    }

    private void DestructTiles() {

        if (exploded != null) exploded.Play();

        Debug.Log("Explode");
        innerHits = Physics2D.OverlapCircleAll(transform.position, innerBlastRadius);

        foreach(Collider2D hit in innerHits) {
            if (hit.gameObject.CompareTag("Player")) {
                hit.gameObject.GetComponent<PlayerStats>().TakeDamage(explosionForce);
            } else if (hit.gameObject.CompareTag("Destructible")) {
                Destroy(hit.gameObject);
            }
        }

        iHits = new List<Collider2D>(innerHits);

        outerHits = Physics2D.OverlapCircleAll(transform.position, outerBlastRadius);
        foreach (Collider2D hit in outerHits) {
            if (!iHits.Contains(hit)) {
                if (hit.gameObject.CompareTag("Player")) {
                    hit.gameObject.GetComponent<PlayerStats>().TakeDamage(explosionForce / 2);
                } else if (hit.gameObject.CompareTag("Destructible")) {
                    Destroy(hit.gameObject);
                }
            }
        }
    }

    protected override void Move() {
        if (!isExplode) {
            base.Move();
        }
    }

    public override void Death() {
        if (deadEffect != null) Instantiate(deadEffect, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    public override void TakeDamage(int damage) {
        isExplode = true;
        StartCoroutine("StartTimer");
    }

    private IEnumerator StartTimer()  {
        if (countdown != null) countdown.Play();
        yield return new WaitForSeconds(explosionTime);
        animator.SetTrigger("Explode");
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, innerBlastRadius);
        Gizmos.DrawWireSphere(transform.position, outerBlastRadius);
    }
}
