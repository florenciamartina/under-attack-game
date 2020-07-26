using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorManager : MonoBehaviour {
    
    [SerializeField] private Tilemap floor;
    [SerializeField] private Transform enemyLayer;
    [SerializeField] private Enemy[] enemies;

    private void Awake() {
        enemies = enemyLayer.GetComponentsInChildren<Enemy>();
    }
    
    // Start is called before the first frame update
    void Start() {
        floor = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update() {

        if (!HasEnemies(enemies)) {
            Debug.Log("Enemies killed");
            DestroyFloor();
        }
        
    }
    private bool HasEnemies(Enemy[] enemies) {
        foreach(Enemy enemy in enemies) {
            if (enemy.gameObject.activeSelf) return true;
        }

        return false;
    }

    private void DestroyFloor() {
        // camera shake and sfx
        Destroy(floor.gameObject);
    }
}
