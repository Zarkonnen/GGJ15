using UnityEngine;
using System.Collections;

public class DestroyDelayedPlatform : MonoBehaviour {

	public float destroyTimer = 3.0f;

	void OnCollisionEnter2D () { //Collision2D collision) {
		Debug.Log ("DestroyDelayedPlatform.OnCollisionEnter: "+gameObject.name);
		/*foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay(contact.point, contact.normal, Color.white);
		}*/
		Invoke("Destroy", destroyTimer);
	}
	

	void Destroy () {
		Destroy(gameObject);
	}
}
