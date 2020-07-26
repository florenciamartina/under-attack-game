using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Minigame : MonoBehaviour {

    [SerializeField] private float y_increment = 5f;
    private Vector2 targetPos;
    [SerializeField] private float speed = 50f;

    [SerializeField] private int health = 3;

    private float minHeight;
    private float maxHeight;

    private int input;

    // Start is called before the first frame update
    void Start() {
        minHeight = transform.position.y - y_increment;
        maxHeight = transform.position.y + y_increment;
    }

    // Update is called once per frame
    void Update() {

        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        input = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) ? 1
            : Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) ? -1 
            : 0;
            

        if (input > 0 && transform.position.y < maxHeight) {
            targetPos = new Vector2(transform.position.x, transform.position.y + y_increment);
        } else if (input < 0 && transform.position.y > minHeight) {
            targetPos = new Vector2(transform.position.x, transform.position.y - y_increment);
        }
        
    }

    public void TakeDamage() {
        health--;
        if (health <= 0) {
            Debug.Log("GAME OVER");
            gameObject.SetActive(false);
        }
    }
}
