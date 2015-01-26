using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

	public KeyCode[] continueKeys = new KeyCode[] {KeyCode.Space, KeyCode.Return, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.UpArrow};
	public int level;

	public void Update () {
		bool continuePressed = false;
		foreach (KeyCode cKey in continueKeys) {
			if (Input.GetKeyDown(cKey)) {
				Debug.Log ("Update test "+cKey+" pressed ");
				continuePressed = true;
			}
		}

		if (continuePressed) {
			Load ();
		}
	}

	public void Load () {
		if (Time.timeSinceLevelLoad > 1.0f) {
			Application.LoadLevel(level);
		}
	}
}
