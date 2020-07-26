using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour {
	[Header("Jump Controls")]
	[SerializeField] private float m_JumpForce = 250f;							// Amount of force added when the player jumps.
	
	[SerializeField] private float m_WallJumpForce = 150f;
	[SerializeField] private float m_WallJumpPushForce = 720f;
	[SerializeField] private float m_WallFriction = -50f;
	[SerializeField] private float wallJumpInterval = 0.2f;
	private float currWallJumpTime = 0f;
	[SerializeField] private float m_DJumpForce = 200f;
	private bool canDoubleJump = true;
	[SerializeField] private AudioSource jumpSound;

	[Header("Basic Movement")]

	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping.
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character

	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;           	// Whether or not the player is grounded.

	[SerializeField] private LayerMask m_WhatIsWall;							// A mask determining what is wall to the character.
	[SerializeField] private Transform m_WallCheck;								// A position marking where to check if the player is touching wall.
	const float k_WallTouchRadius = .2f;	// Radius of the overlap circle to determine if touching wall.
	private bool m_TouchingWall;			// Whether or not the player is touching wall.

	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private void Awake() {
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void FixedUpdate() {
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject) {
				m_Grounded = true;
				canDoubleJump = true; // Added
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}

		m_TouchingWall = Physics2D.OverlapCircle(m_WallCheck.position, k_WallTouchRadius, m_WhatIsWall);

		// if (m_TouchingWall) {
		// 	m_Grounded = false;
        // }
		// isJumping = false;
	}

	private void Update() {
		if (m_Grounded) {
			currWallJumpTime = 0;
		} else if (!m_Grounded && m_TouchingWall) {
			currWallJumpTime -= Time.deltaTime;
		}
	}

	public void Move(float move, bool jump) {

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl) {

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}

		// Double jump
		if (!m_Grounded && canDoubleJump && jump) {
			m_Rigidbody2D.AddForce(new Vector2(m_Rigidbody2D.velocity.x, m_DJumpForce));
			canDoubleJump = false;
			jumpSound.Play();
		}

		// If the player is touching the wall and jumps...
		if (!m_Grounded && m_TouchingWall && jump) {

			if (move == 0 && currWallJumpTime <= 0) {
				WallJump();
			} else if ((m_FacingRight && move > 0) || (!m_FacingRight && move < 0)) {
				m_Rigidbody2D.AddForce(new Vector2(0f, m_WallFriction));
			}

		// Single Jump
		} else if (m_Grounded && jump) {
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			jumpSound.Play();
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;
		
		transform.Rotate(0f, 180f, 0f);
	}

	private void WallJump () {
		currWallJumpTime = wallJumpInterval;
		float force = m_FacingRight ? -m_WallJumpPushForce : m_WallJumpPushForce;
        m_Rigidbody2D.AddForce(new Vector2 (force, m_WallJumpForce));
		Flip();
		jumpSound.Play();
    }

	private void OnCollisionEnter2D(Collision2D other) {

		// Move a few space backwards if hit enemy.
        if (other.gameObject.tag == "Enemy") {
			m_Rigidbody2D.velocity = Vector2.zero;
			if (other.gameObject.transform.position.x > gameObject.transform.position.x) {
				m_Rigidbody2D.AddForce(new Vector2(-100f, 0f));
			} else {
				m_Rigidbody2D.AddForce(new Vector2(100f, 0f));
			}
        }
    }

	public bool IsGrounded() {
		return m_Grounded;
	}

	public bool isTouchingWall() {
		return m_TouchingWall;
	}
}