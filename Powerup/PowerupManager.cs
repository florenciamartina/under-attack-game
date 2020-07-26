using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour {
    [SerializeField] private GameObject costumes;
    [SerializeField] private GameObject shield;

    [SerializeField] private GameObject NKCBullet;

    private PlayerStats stats;
    private PlayerCombat combat;
    private GameObject bullet;
    private Animator animator;

    // private float currTime = 0f;

    private void Start() {
        animator = GetComponent<Animator>();
        stats = GetComponent<PlayerStats>();
        combat = GetComponent<PlayerCombat>();
    }

    public void SetCostume(Sprite costume) {
        costumes.GetComponent<SpriteRenderer>().sprite = costume;
    }

    public void ActivateShield() {
        shield.SetActive(true);
        shield.GetComponent<Shield>().ResetShield();
    }

    public IEnumerator ActivateNKC(float time) {

        stats.ActivateNKC();
        bullet = combat.GetBullet();
        combat.ChangeBullet(NKCBullet);
        animator.SetBool("NKC", true);

        yield return new WaitForSeconds(time);

        animator.SetBool("NKC", false);
        combat.ChangeBullet(bullet);
        stats.DeactivateNKC();
    }
}
