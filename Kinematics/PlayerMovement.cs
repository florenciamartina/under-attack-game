using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    private CharacterController2D controller;
    private Animator animator;
    private bool jump = false;
    public bool canMove = true;

    [SerializeField] private float runSpeed = 40f;
    private float horizontalMove = 0f;

    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource playerHurt;

    // Start is called before the first frame update
    private void Start() {
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update() {

        if (!canMove) return;

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;  

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump")) {
            jump = true;
            animator.SetBool("Jump", true);
            jumpSound.Play();
        }

        if (Input.GetButtonDown("Fire1")) {
            animator.SetTrigger("Attack");
        }

        if (Input.GetButtonDown("Fire2")) {
            animator.SetBool("Shoot", true);
        }
    }

    public void OnLanding() {
        animator.SetBool("Jump", false);
    }
    private void FixedUpdate() {
        if(!canMove) return;

        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
        animator.SetBool("Shoot", false);
        animator.SetBool("Hurt", false);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy") {
            Hurt();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "DeathZone") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Hurt() {
        playerHurt.Play();
		animator.SetBool("Hurt", true);
    }
}
