using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PlayerPusher : MonoBehaviour {

	public Vector3 pushVector;
	public bool enabled;
	public List<GameObject> collidedItems;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		for (int i=0; i<collidedItems.Count; i++) {
			if (collidedItems[i].rigidbody2D != null) {
				collidedItems[i].rigidbody2D.AddForce(pushVector);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("enter: "+other);
		if (!collidedItems.Contains(other.gameObject)) {
			collidedItems.Add(other.gameObject);
		}
	}
	void OnTriggerExit2D(Collider2D other) {
		Debug.Log ("exit: "+other);
		if (collidedItems.Contains(other.gameObject)) {
			collidedItems.Remove(other.gameObject);
		}
	}
}
