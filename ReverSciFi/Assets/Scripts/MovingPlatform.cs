using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	//public Vector3 startPos;
	//public Vector3 endPos;
	private Vector3 sourcePos;
	public Vector3 offsetPos = Vector3.up;
	public float movementTime = 2.0f;

	void Start () {
		sourcePos = transform.position;
	}

	void FixedUpdate () {
		float t = (Mathf.Sin(Time.time)+1.0f)/movementTime;
		rigidbody2D.MovePosition( sourcePos + Vector3.Lerp(Vector3.zero, offsetPos, t));
	}
}
