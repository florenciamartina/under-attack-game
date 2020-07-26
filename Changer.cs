using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changer : MonoBehaviour {

    private string[] bulletColor = {"Default", "Yellow", "Red", "Orange"};
    [SerializeField] private GameObject bullet;
    private GameObject prevBullet;

    [SerializeField] private ParticleSystem[] activateEffect;
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
        animator.SetTrigger(bulletColor[bullet.GetComponent<Bullet>().GetBulletType()]);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerCombat combat = other.gameObject.GetComponent<PlayerCombat>();
            if (combat != null) {
                Debug.Log("Player entered changer");

                Instantiate(activateEffect[bullet.GetComponent<Bullet>().GetBulletType()], 
                            transform.position, transform.rotation);

                prevBullet = combat.GetBullet();
                combat.ChangeBullet(bullet);
                bullet = prevBullet;

                animator.SetTrigger(bulletColor[bullet.GetComponent<Bullet>().GetBulletType()]);
            }
        }
    }
}
