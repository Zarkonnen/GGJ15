using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour {
	public bool repaired = false;
	public bool flickering = false;
	public bool on = false;

	void Update () {
		if (!repaired && flickering && Random.Range (0, 500) < 10) {
			flickering = false;
			GetComponent<SpriteRenderer>().enabled = false;
		}

		if (!repaired && !flickering && Random.Range (0, 3000) < 10) {
			flickering = true;
			GetComponent<SpriteRenderer>().enabled = true;
			GetComponent<AudioSource>().Play();
		}
		
		if (!repaired && flickering && Random.Range (0, 100) < 10) {
			on = !on;
			GetComponent<SpriteRenderer>().enabled = on;
		}

		if (repaired) {
			GetComponent<SpriteRenderer>().enabled = false;
		}
	}
}
