using UnityEngine;
using System.Collections;

public class DestroyDelayedPlatform : MonoBehaviour {

	public float destroyTimer = 3.0f;


	void OnCollisionEnter2D (Collision2D collision) {
//		Debug.Log ("DestroyDelayedPlatform.OnCollisionEnter: "+gameObject.name+" collision.collider.tag "+collision.collider.tag);

		if (collision.collider.tag == "Player") {
			Invoke("Destroy", destroyTimer);
		}
		/*foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay(contact.point, contact.normal, Color.white);
		}*/

	}
	

	void Destroy () {
		Destroy(gameObject);
	}
}
