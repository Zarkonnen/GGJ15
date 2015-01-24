using UnityEngine;
using System.Collections;

public class ExcitingArea : MonoBehaviour {
	private bool active;
	
	void Update() {
		if (active) {
			AudioManager.Instance.Exciting ();
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