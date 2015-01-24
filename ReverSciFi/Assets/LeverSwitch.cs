using UnityEngine;
using System.Collections;

public class LeverSwitch : MonoBehaviour {
	public bool on = false;

	private Animator anim;
	private float timeSinceSwitch;

	void Update() {
		timeSinceSwitch += Time.deltaTime;
	}

	void Start() {
		anim = transform.GetComponentInChildren<Animator>();
		anim.SetBool ("On", on);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player" && timeSinceSwitch > 1) {
			on = !on;
			anim.SetBool ("On", on);
			timeSinceSwitch = 0;
		}
	}

	void OnTriggerExit2D(Collider2D other) {

	}
}
