using UnityEngine;
using System.Collections;

public class AudioControlTest : MonoBehaviour {
	private float t = 0;
	private bool combatOn = false;
	private bool combatOff = false;

	// Use this for initialization
	void Start () {
		Fabric.EventManager.Instance.PostEvent ("Music");
	}
	
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		if (t > 3 && !combatOn) {
			Fabric.EventManager.Instance.SetParameter("Music", "Combat", 1.0f, null);
			combatOn = true;
		}
		if (t > 8 && !combatOff) {
			Fabric.EventManager.Instance.SetParameter("Music", "Combat", 0.0f, null);
			combatOff = true;
		}
	}
}
