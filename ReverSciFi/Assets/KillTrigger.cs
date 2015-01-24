using UnityEngine;
using System.Collections;

public class KillTrigger : MonoBehaviour {

	public GameObject killObject;

	// Use this for initialization
	void OnTriggerEnter2D (Collider2D other) {
		Debug.Log ("KillTrigger.OnCollisionEnter2D: "+killObject.name);
		if (other.tag == "Player") {
			killObject.SendMessage("Death", SendMessageOptions.DontRequireReceiver);
		}
	}
}
