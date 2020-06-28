using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBacteria : Enemy
{
    [Header("Clone")]
    [SerializeField] private GameObject purpleBacteria;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Bullet"))
        {
            SpawnEnemy();
            Destroy(gameObject);
        }
    }

    private void SpawnEnemy() {
        float x_pos = this.transform.position.x;
        float y_pos = this.transform.position.y;
        Vector2 range_pos = new Vector2(Random.Range(x_pos - 2, x_pos + 2), y_pos);
        Instantiate(gameObject, range_pos, transform.rotation);
        Instantiate(gameObject, range_pos, transform.rotation);
    }
}
