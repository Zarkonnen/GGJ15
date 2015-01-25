using UnityEngine;
using System.Collections;

public class SoundTrigger : MonoBehaviour {
	private bool played = false;
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player" && !played) {
			GetComponent<AudioSource>().Play();
			played = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		
	}
}
