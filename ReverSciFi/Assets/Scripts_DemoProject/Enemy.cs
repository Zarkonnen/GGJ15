using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public float moveSpeed = 2f;		// The speed the enemy moves at.
	//public int HP = 2;					// How many times the enemy can be hit before it dies.
	//public Sprite deadEnemy;			// A sprite of the enemy when it's dead.
	//public Sprite damagedEnemy;			// An optional sprite of the enemy when it's damaged.
	public AudioClip[] deathSounds;		// An array of audioclips that can play when the enemy dies.
	public AudioClip[] attackSounds;
	//public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.
	//public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
	//public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying
	public GameObject[] spawnings;

	private float stopTimer;
	public float lastCollisionTime = 0.0f;
	public int lastCollisionCount = 0;

	private SpriteRenderer ren;			// Reference to the sprite renderer.
	//private GameObject frontCheckGO;
	private Transform frontCheck;		// Reference to the position of the gameobject used for checking if something is in front.
	private bool dead = false;			// Whether or not the enemy is dead.
	private Score score;				// Reference to the Score script.

	//public GameObject splash;
	Animator anim;

	
	void Awake() {
		// Setting up the references.
		ren = transform.Find("body").GetComponent<SpriteRenderer>();
		frontCheck = transform.FindChild("frontCheck");
		if (frontCheck == null) {
			Debug.Log ("FrontCheck not found!");
		}
		anim = GetComponent<Animator>();
//		score = GameObject.Find("Score").GetComponent<Score>();
	}

	void FixedUpdate () {
		if (dead)
			return;

		// Create an array of all the colliders in front of the enemy.
		if (frontCheck == null) {		
			return;
		}
		Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position);
//		Debug.Log ("Enemy.FixedUpdate: frontHits "+frontHits.Length);

		// Check each of the colliders.
		foreach(Collider2D c in frontHits) {
//			Debug.Log ("Enemy.FixedUpdate: collided with "+c.tag);

			// If any of the colliders is an Obstacle...
			if ((c.tag == "Obstacle") || (c.tag == "Crate") || (c.tag == "ground") || c.gameObject.layer == LayerMask.NameToLayer("Ground")) {
				if (stopTimer > 0) {
					// do nothing
				} else {
					// ... Flip the enemy and stop checking the other colliders.
					Flip ();

					// check for lots of recent collision
					if ((Time.time - lastCollisionTime) < 0.1f) {
						lastCollisionCount += 1;
					} else {
						lastCollisionCount = 0;
					}
					lastCollisionTime = Time.time;
					if (lastCollisionCount > 3) {
						Debug.Log ("Enemy.FixedUpdate: "+lastCollisionCount+" with "+c.tag+" "+c.name);
						stopTimer = 3.0f;
						lastCollisionCount = 0;
					}
				}

				break;
			}
			if (c.tag == "Enemy") {

				if (Random.Range(0.0f,1.0f) > 0.5f) {
					Flip ();
				} else {
					stopTimer = Random.Range(0.1f, 0.3f);
				}
				Debug.Log ("Enemy.FixedUpdate: collided with enemy stop for "+stopTimer); 
				break;
			}
		}

		if (stopTimer > 0.0f) {
			stopTimer -= Time.deltaTime;
			// dont move while stopped
			rigidbody2D.velocity = new Vector2(transform.localScale.x * 0, rigidbody2D.velocity.y);	
		} else {
			// Set the enemy's velocity to moveSpeed in the x direction.
			rigidbody2D.velocity = new Vector2(transform.localScale.x * moveSpeed, rigidbody2D.velocity.y);	
			
			if (anim != null) {
				anim.SetFloat("Speed", rigidbody2D.velocity.magnitude);
			}
		}


		// If the enemy has one hit point left and has a damagedEnemy sprite...
/*		if(HP == 1 && damagedEnemy != null)
			// ... set the sprite renderer's sprite to be the damagedEnemy sprite.
			ren.sprite = damagedEnemy;*/
			
		// If the enemy has zero or fewer hit points and isn't dead yet...
		/*if(HP <= 0 && !dead)
			// ... call the death function.
			StartCoroutine(Death ());*/
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			Debug.Log ("Enemy.OnCollisionEnter2D: collided with player");
			coll.gameObject.GetComponent<PlayerControl>().Death ();

			if (attackSounds.Length>0) {
				int i = Random.Range(0, attackSounds.Length);
				AudioSource.PlayClipAtPoint(attackSounds[i], transform.position);
			}
		}
	}

	/*public void Hurt()
	{
		// Reduce the number of hit points by one.
		HP--;
	}*/
	
	IEnumerator Death() {
		if (!dead) {
			// Find all of the sprite renderers on this object and it's children.
			/*SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();

			// Disable all of them sprite renderers.
			foreach (SpriteRenderer s in otherRenderers) {
				s.enabled = false;
			}*/

			// Re-enable the main sprite renderer and set it's sprite to the deadEnemy sprite.
			//ren.enabled = true;
			//ren.sprite = deadEnemy;

			// Increase the score by 100 points
			//score.score += 100;

			// Set dead to true.
			dead = true;
			if (anim != null) {
				anim.SetFloat("Speed", 0);
				anim.SetBool("Dead", true);
			}

			// Allow the enemy to rotate and spin it by adding a torque.
			/*rigidbody2D.fixedAngle = false;
			rigidbody2D.AddTorque(Random.Range(deathSpinMin,deathSpinMax));*/

			rigidbody2D.isKinematic = true;
			Collider2D[] cols = GetComponents<Collider2D>();
			foreach(Collider2D c in cols) {
				c.isTrigger = true;
			}
			
			// Find all of the colliders on the gameobject and set them all to be triggers.
			/*
			GameObject splashInstance = Instantiate(splash, transform.position, transform.rotation) as GameObject;
			splashInstance.SetActive(false);*/

			// Play a random audioclip from the deathClips array.
			if (deathSounds.Length > 0) {
				int i = Random.Range(0, deathSounds.Length);
				AudioSource.PlayClipAtPoint(deathSounds[i], transform.position);
			}

			yield return new WaitForSeconds(1.5f);

			switch (spawnings.Length) {
			case 1:
				Instantiate(spawnings[0], transform.position, transform.rotation);
				break;
			case 2:
				Instantiate(spawnings[0], transform.position-Vector3.right, transform.rotation);
				Instantiate(spawnings[1], transform.position+Vector3.right, transform.rotation);
				break;
			case 3:
				Instantiate(spawnings[0], transform.position-Vector3.right, transform.rotation);
				Instantiate(spawnings[1], transform.position+Vector3.right, transform.rotation);
				Instantiate(spawnings[2], transform.position, transform.rotation);
				break;
			}

			Destroy(gameObject);

			//splashInstance.SetActive(true);

			//rigidbody2D.isKinematic = true;

			//rigidbody.isKinematic = true;



			// Create a vector that is just above the enemy.
			/*Vector3 scorePos;
			scorePos = transform.position;
			scorePos.y += 1.5f;*/

			// Instantiate the 100 points prefab at this point.
			//Instantiate(hundredPointsUI, scorePos, Quaternion.identity);
		}
	}


	public void Flip() {
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
	}
}
