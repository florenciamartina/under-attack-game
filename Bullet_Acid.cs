using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet_Acid : Bullet_Enemy {

    [Header("Terrain Attack")]
    [SerializeField] private Tilemap platform;
    [SerializeField] private Tilemap acid;
    [SerializeField] private AnimatedTile acidPool;
    [SerializeField] private AudioSource acidSound;
    private Vector3Int initialPos;

    private Vector3Int centerCellPos;

    private Vector3Int tilePos;
    private Vector3Int aboveTilePos;

    // private void Awake() {
    //     platform = GameObject.Find("Platform").GetComponent<Tilemap>();
    //     acid = GameObject.Find("Acid").GetComponent<Tilemap>();
    // }
    protected override void Start() {
        base.Start();
    }

    protected override void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Collectible") return;

        AttackTerrain();
    }

    private void AttackTerrain() {

        // StartCoroutine(AcidSound());

        acidSound.Play();

        centerCellPos = platform.WorldToCell(transform.position);

        // if (centerCellPos.y <= initialPos.y - 1) return;

        for (int i = centerCellPos.x - 2; i <= centerCellPos.x + 3; i++) {
            aboveTilePos = new Vector3Int(i, centerCellPos.y, centerCellPos.z);
            tilePos = new Vector3Int(i, centerCellPos.y - 1, centerCellPos.z);
            if (platform.HasTile(tilePos) && !acid.HasTile(aboveTilePos) && !platform.HasTile(aboveTilePos)) {
                // platform.SetTile(pos, acidTile);
                // StartCoroutine("Degrade");
                platform.SetTile(tilePos, null);
                acid.SetTile(tilePos, acidPool);
            }
        }

        acidSound.Stop();

        Destroy(gameObject);
    }

    public void SetTerrain(Tilemap platform, Tilemap acid) {
        this.platform = platform;
        this.acid = acid;
    }

    // private IEnumerator AcidSound() {
    //     if (acidSound != null) {
    //         acidSound.Play();

    //         yield return new WaitForSeconds(1f);

    //         acidSound.Stop();
    //     }
    // }
}
