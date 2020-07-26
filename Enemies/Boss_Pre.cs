using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Pre : MonoBehaviour {

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float attackInterval = 5f;
    private float currTime;
    private int damage = 35;
    private RaycastHit2D hit;
    private GameObject target;

    // Start is called before the first frame update
    void Start() {
        currTime = 3f;
    }

    // Update is called once per frame
    void Update() {
        if (currTime <= 0) {
            Attack();
        } else {
            currTime -= Time.deltaTime;
        }
    }

    private void Attack() {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        currTime = attackInterval;
    }
}
