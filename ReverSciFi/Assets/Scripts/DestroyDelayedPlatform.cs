using UnityEngine;
using System.Collections;

public class DestroyDelayedPlatform : MonoBehaviour {
	public float destroyTimer = 0.6f;
	private bool destroying;

	void OnCollisionEnter2D (Collision2D collision) {
//		Debug.Log ("DestroyDelayedPlatform.OnCollisionEnter: "+gameObject.name+" collision.collider.tag "+collision.collider.tag);

		if (!destroying && collision.collider.tag == "Player") {
			Invoke("Destroy", destroyTimer);
			transform.FindChild("Crack").GetComponentInChildren<Animator>().SetTrigger("Start");
			destroying = true;
		}

		/*foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay(contact.point, contact.normal, Color.white);
		}*/

	}
	

	void Destroy () {
		Destroy(gameObject);
	}
}
