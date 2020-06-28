using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    [SerializeField] private GameObject[] dialogues;
    private int index = 0;

    private bool facingRight = false;
    private Rigidbody2D rb;

    private Collider2D coll;
    private float speed = 5f;

    [SerializeField] private GameObject nutrigem;
    [SerializeField] private GameObject bullet;

    [SerializeField] private Transform firePoint;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        foreach(GameObject dialogue in dialogues) {
            dialogue.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (index == dialogues.Length) {
            Destroy(gameObject);
        }

        if (index == 0 && dialogues[index] == null) {
                Flip();
                rb.AddForce(new Vector2(0f, 350f));
                rb.velocity = new Vector2(speed, rb.velocity.y);
                index++;
        } else if (index == 1 && nutrigem == null) {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            index++;
        } else if (index == 3) {
            if (dialogues[index] == null) {
                index++;
            }
        } else if (index == 4) {
            transform.SetPositionAndRotation(new Vector3(47f, 14f, 0f), Quaternion.identity);

            if (dialogues[index] == null) {
                index++;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Player is in guide trigger zone.");

            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);

            if ((other.transform.position.x < transform.position.x && facingRight) ||
             (other.transform.position.x > transform.position.x && !facingRight)) {
                Flip();
            }

            dialogues[index].SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            dialogues[index].SetActive(true);
            
            GetComponent<SpriteRenderer>().enabled = false;
            index++;
            transform.SetPositionAndRotation(new Vector3(36f, 3f, 0f), Quaternion.identity);
        }
    }

    private void Flip() {
        transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }

}
