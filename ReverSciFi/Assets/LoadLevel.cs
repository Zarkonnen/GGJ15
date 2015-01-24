using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

	public int level;

	public void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			Load ();
		}
	}

	public void Load () {
		Application.LoadLevel(level);
	}
}
