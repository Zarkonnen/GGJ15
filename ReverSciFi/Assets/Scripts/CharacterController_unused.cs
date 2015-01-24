using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	public KeyCode leftKey = KeyCode.A;
	public KeyCode rightKey = KeyCode.D;
	public KeyCode jumpKey = KeyCode.Space;
	public KeyCode sneakKey = KeyCode.LeftShift;

	public float fullVelocity = 10.0f;
	public float sneakVelocity = 5.0f;
	public float jumpVelocity = 25.0f;

	private Transform groundCheck;
	public bool grounded = false;			// Whether or not the player is grounded.
	private Animator anim;					// Reference to the player's animator component.
	//[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
	//[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.

	public float vSpeed = 0.0f;
	public float prevVSpeed = 0.0f;

	// Use this for initialization
	void Start () {
		groundCheck = transform.Find("groundCheck");
		anim = GetComponent<Animator>();
	}

	void Update () {
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  
		
		// If the jump button is pressed and the player is grounded then the player should jump.
		//if(Input.GetButtonDown("Jump") && grounded) {
		if (Input.GetKeyDown(jumpKey) && grounded) {
			Debug.Log ("jumpkey pressed");
			jump = true;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (grounded) {
			// movement of the character
			vSpeed = 0.0f;
			if (Input.GetKey(leftKey)) {
				if (Input.GetKey(sneakKey)) {
					vSpeed = -sneakVelocity;
				} else {
					vSpeed = -fullVelocity;
				}
				if (facingRight) {
					Flip();
				}
			}
			if (Input.GetKey(rightKey)) {
				if (Input.GetKey(sneakKey)) {
					vSpeed = sneakVelocity;
				} else {
					vSpeed = fullVelocity;
				}
				if (!facingRight) {
					Flip();
				}
			}
		}

		//rigidbody2D.inertia = 
		//rigidbody2D.velocity = new Vector2(vSpeed, rigidbody2D.velocity.y);
		float physicVVelosity = - prevVSpeed + rigidbody2D.velocity.x;
		Debug.Log ("velocity rigidX "+rigidbody2D.velocity.x+" prevX "+prevVSpeed+" new "+(vSpeed + physicVVelosity));
		prevVSpeed = vSpeed;
		//if (vSpeed 
		float newVVelocity = vSpeed - physicVVelosity;
		rigidbody2D.velocity = new Vector2(newVVelocity, rigidbody2D.velocity.y);

		// apply movement to the animation
		anim.SetFloat("Speed", Mathf.Abs(vSpeed));

		// If the player should jump...
		if(jump) {
			Debug.Log ("doJump");
			// Set the Jump animator trigger parameter.
			anim.SetTrigger("Jump");
			
			// Play a random jump audio clip.
			//int i = Random.Range(0, jumpClips.Length);
			//AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);
			
			// Add a vertical force to the player.
			rigidbody2D.AddForce(new Vector2(0f, jumpVelocity), ForceMode2D.Impulse);
			
			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}
	}

	void Flip () {
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
