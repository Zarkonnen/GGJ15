using UnityEngine;
using System.Collections;

public class DangerArea : MonoBehaviour {
	private bool active;
	
	void Update() {
		if (active) {
			AudioManager.Instance.Danger ();
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			active = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			active = false;
		}
	}
}