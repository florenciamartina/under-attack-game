using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : Enemy
{
    [Header("Enemy Shooting")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject FirePointL;
    [SerializeField] private GameObject FirePointR;
    [SerializeField] private BoxCollider2D shootingRange;
    [SerializeField] private float timeBetweenShots = 2f;
    private float currTime;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        currTime = 0;
        
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
        
        // Shooting
        if (currTime > 0) {
            currTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            // Shooting
            if (currTime <= 0) {
                currTime = timeBetweenShots;

                if (facingLeft) {

                    if (other.transform.position.x < transform.position.x) {
                        Instantiate(bullet, FirePointL.transform.position, Quaternion.identity)
                            .transform.Rotate(0f, 180f, 0f);
                    } else {
                        Instantiate(bullet, FirePointR.transform.position, Quaternion.identity);
                    }

                } else {

                    if (other.transform.position.x < transform.position.x) {
                        Instantiate(bullet, FirePointR.transform.position, Quaternion.identity)
                        .transform.Rotate(0f, 180f, 0f);
                    } else {
                        Instantiate(bullet, FirePointL.transform.position, Quaternion.identity);
                    }

                }
            }
        }
    }
}
