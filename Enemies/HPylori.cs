using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HPylori : Enemy {
    [SerializeField] private Transform firePoint;

    [SerializeField] private Tilemap platform;

    [SerializeField] private Tilemap acid;
    [SerializeField] private Bullet_Enemy enzymeBullet;

    [SerializeField] private Bullet_Enemy bullet;

    private Bullet_Enemy shoot;
    [SerializeField] private float attackInterval = 4f;
    private float currTime = 0;
    [SerializeField] private LayerMask m_WhatIsWall;							// A mask determining what is wall to the character.
	[SerializeField] private Transform m_WallCheck;								// A position marking where to check if the player is touching wall.
	const float k_WallTouchRadius = .05f;	// Radius of the overlap circle to determine if touching wall.
	private bool m_TouchingWall;			// Whether or not the player is touching wall.

    private Vector3Int initialPos;

    private Vector3Int centerCellPos;

    protected override void Start() {
        base.Start();
        initialPos = platform.WorldToCell(transform.position);
        facingLeft = true;
        currTime = 0;
    }

    protected override void Update() {
        base.Update();
        currTime -= Time.deltaTime;

        m_TouchingWall = Physics2D.Raycast(m_WallCheck.position, Vector2.left, k_WallTouchRadius, m_WhatIsWall);
        
        if (m_TouchingWall) {
            Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            
            if (other.transform.position.x > transform.position.x && facingLeft ||
                other.transform.position.x < transform.position.x &&!facingLeft) Flip();

            Attack();
        }
    }

    private void Attack() {
        if (currTime <= 0) {
            currTime = attackInterval;

            if (enzymeBullet != null) {
                shoot = transform.position.y <= initialPos.y - 1 ? bullet : enzymeBullet;

                if (shoot == enzymeBullet) enzymeBullet.GetComponent<Bullet_Acid>().SetTerrain(platform, acid);
            } else {
                shoot = bullet;
            }

            Instantiate(shoot, firePoint.position, firePoint.rotation);
        }
    }

    // private IEnumerator Degrade() {
    //     yield return new WaitForSeconds(3f);
    // }

    public override void Death() {
        //enemyDead.Play();
        if (deadEffect != null) Instantiate(deadEffect, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }
}