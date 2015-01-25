using UnityEngine;
using System.Collections;

public class RepairConsole : MonoBehaviour {
	public bool repaired = false;
	public bool on = false;
	public GameObject activateText;

	void Start () {
		activateText.SetActive(false);
	}

	void Update () {
		if (!repaired && Random.Range (0, 100) < 10) {
			on = !on;
			transform.FindChild("repair").GetComponent<SpriteRenderer>().enabled = on;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player" && !repaired) {
			GetComponent<ParticleSystem>().Stop ();
			repaired = true;
			transform.FindChild("electric").GetComponent<AudioSource>().Stop();
			transform.FindChild("repair").GetComponent<AudioSource>().Play();
			transform.FindChild("repair").GetComponent<SpriteRenderer>().enabled = true;
			activateText.SetActive(true);
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		
	}
}
