using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBG : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private GameObject bg;
    private float clampPos;
    private Vector3 startPos;

    private float newPos;
    // Start is called before the first frame update
    void Start() {
        startPos = transform.position;
        clampPos = bg.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    // void Update() {
    //     transform.Translate(Vector2.left * speed * Time.deltaTime);

    //     if (transform.position.x < -width) {
    //         transform.position = (Vector2) transform.position + new Vector2(width * 2f, 0);
    //     }
    // }

    private void FixedUpdate() {
        newPos = Mathf.Repeat(Time.time * speed, clampPos);
        transform.position = startPos + Vector3.left * newPos;
    }

    public void SetSpeed(float speed) {
        this.speed = speed;
    }
}
