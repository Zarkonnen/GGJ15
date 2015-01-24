using UnityEngine;
using System.Collections;

public class KillTrigger : MonoBehaviour {

	public GameObject killObject;

	// Use this for initialization
	void OnTriggerEnter2D () {
		Debug.Log ("KillTrigger.OnCollisionEnter2D: "+killObject.name);
		killObject.SendMessage("Death", SendMessageOptions.DontRequireReceiver);
	}
}
