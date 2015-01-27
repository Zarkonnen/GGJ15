using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

	public KeyCode[] leftKeys = new KeyCode[] {KeyCode.A, KeyCode.LeftArrow};
	public KeyCode[] rightKeys = new KeyCode[] {KeyCode.D, KeyCode.RightArrow};
	public KeyCode[] jumpKeys = new KeyCode[] {KeyCode.Space, KeyCode.UpArrow};
	public KeyCode[] sneakKeys = new KeyCode[] {KeyCode.LeftShift};
	public float sneakVelocity = 0.5f;
	public float walkVelocity = 1.5f;
	public float animatedVelocity = 0.0f;
	[HideInInspector]
	public bool facingRight = false;
	// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;
	private bool dead = false;
	// Condition for whether the player should jump.


	public float moveForce = 365f;
	// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;
	// The fastest the player can travel in the x axis.
	public AudioClip[] jumpSounds;
	// Array of clips for when the player jumps.
	public float jumpForce = 1000f;
	private float lastTimeGrounded = 0.0f;
  
	// Amount of force added when the player jumps.
	public AudioClip[] taunts;
	public string[] textTaunts;
	// Array of clips for when the player taunts.
	public float tauntProbability = 50f;
	// Chance of a taunt happening.
	public float tauntDelay = 1f;
	// Delay for when the taunt should happen.

	public AudioClip[] deathSounds;
	private int tauntIndex;
	// The index of the taunts array indicating the most recent taunt.
	private Transform groundCheck;
	// A position marking where to check if the player is grounded.
	//private bool grounded = false;
	// Whether or not the player is grounded.
	private Animator anim;
	// Reference to the player's animator component.


	void Awake () {
		// Setting up references.
		groundCheck = transform.Find ("groundCheck");
		anim = GetComponent<Animator> ();

		rigidbody2D.GetComponent<CircleCollider2D> ().sharedMaterial.friction = 1.0f;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnDrawGizmosSelected() {
		CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
		Vector2 v2 = new Vector2(circleCollider.center.x + transform.position.x, circleCollider.center.y + transform.position.y - 0.08f);
		float r = circleCollider.radius - 0.04f;
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(v2, r);
	}

	void Update () {
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		/*if ( Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground")) ) {
			lastTimeGrounded = Time.time;
		}*/

		CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
		Vector2 v2 = new Vector2(circleCollider.center.x + transform.position.x, circleCollider.center.y + transform.position.y - 0.08f);
		float r = circleCollider.radius - 0.04f;
		if ( Physics2D.OverlapCircle(v2, r, 1 << LayerMask.NameToLayer ("Ground")) ) {
			lastTimeGrounded = Time.time;
		}


		bool jumpPressed = false;
		foreach (KeyCode jKey in jumpKeys) {
			jumpPressed = jumpPressed || Input.GetKey(jKey);
		}

		// If the jump button is pressed and the player is grounded then the player should jump.
		if (jumpPressed && ((Time.time - lastTimeGrounded) < 0.05f) && !dead) {
			jump = true;
		}
	}

	void FixedUpdate () {
		if (dead) {
			return;
		}

		bool leftPressed = false;
		foreach (KeyCode lKey in leftKeys) {
			leftPressed = leftPressed || Input.GetKey(lKey);
		}
		bool rightPressed = false;
		foreach (KeyCode rKey in rightKeys) {
			rightPressed = rightPressed || Input.GetKey(rKey);
		}
		bool sneakPressed = false;
		foreach (KeyCode sKey in sneakKeys) {
			sneakPressed = sneakPressed || Input.GetKey(sKey);
		}


		// horizontal movement controly by keyboard
		float h = 0;
		if (leftPressed) {
			if (sneakPressed) {
				h = -walkVelocity;
			} else {
				h = -sneakVelocity;
			}
		}
		if (rightPressed) {
			if (sneakPressed) {
				h = walkVelocity;
			} else {
				h = sneakVelocity;
			}
		}

		// control character by animation
		if (animatedVelocity != 0) {
			h = animatedVelocity;
		}

		// change friction on movement
		if (Mathf.Abs (h) > 0.1f) {
			rigidbody2D.GetComponent<CircleCollider2D> ().sharedMaterial.friction = 1.0f;
			//h = h / h * Mathf.Sign(h);
		} else {
			rigidbody2D.GetComponent<CircleCollider2D> ().sharedMaterial.friction = 10.0f;
		}

		// The Speed animator parameter is set to the absolute value of the horizontal input.
		anim.SetFloat ("Speed", Mathf.Abs (h));

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if (h * rigidbody2D.velocity.x < maxSpeed) {
			// ... add a force to the player.
			rigidbody2D.AddForce (Vector2.right * h * moveForce);
		}
		// If the player's horizontal velocity is greater than the maxSpeed...
		if (Mathf.Abs (rigidbody2D.velocity.x) > maxSpeed) {
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2 (Mathf.Sign (rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		}


		// If the input is moving the player right and the player is facing left...
		if (h > 0 && !facingRight) {
			// ... flip the player.
			Flip ();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (h < 0 && facingRight) {
			// ... flip the player.
			Flip ();
		}

		// If the player should jump...
		if (jump) {
			// Set the Jump animator trigger parameter.
			//anim.SetTrigger("Jump");

			// Play a random jump audio clip.
			//int i = Random.Range (0, jumpClips.Length);
			if (jumpSounds.Length > 0)
				AudioSource.PlayClipAtPoint (jumpSounds [Random.Range (0, jumpSounds.Length)], transform.position);
			/*audio.clip = 
			audio.Play ();*/

			// Add a vertical force to the player.
			rigidbody2D.AddForce (new Vector2 (0f, jumpForce - Mathf.Min (rigidbody2D.velocity.y/Time.deltaTime)));

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

	public IEnumerator Taunt () {
		// Check the random chance of taunting.
		float tauntChance = Random.Range (0f, 100f);
		if (tauntChance > tauntProbability) {
			// Wait for tauntDelay number of seconds.
			yield return new WaitForSeconds (tauntDelay);

			// If there is no clip currently playing.
			if (!audio.isPlaying) {
				// Choose a random, but different taunt.
				tauntIndex = TauntRandom ();

				// Play the new taunt.
				audio.clip = taunts [tauntIndex];
				audio.Play ();
			}
		}
	}

	public IEnumerator DeathCoroutine () {
		yield return new WaitForSeconds (3.0f);
		Application.LoadLevel ("101_DEATH");
	}

	public void Death () {
		if (!dead) {
			dead = true;

			anim.SetBool ("Dead", true);
			anim.SetFloat ("Speed", 0);

			if (deathSounds.Length > 0) {
				audio.clip = deathSounds [Random.Range (0, deathSounds.Length)];
				audio.Play ();
			}

			StartCoroutine (DeathCoroutine ());
		}
	}

	int TauntRandom () {
		// Choose a random index of the taunts array.
		int i = Random.Range (0, taunts.Length);

		// If it's the same as the previous taunt...
		if (i == tauntIndex)
			// ... try another random taunt.
			return TauntRandom ();
		else
			// Otherwise return this index.
			return i;
	}
}
