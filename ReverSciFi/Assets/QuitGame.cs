using UnityEngine;

public class QuitGame : MonoBehaviour
{
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyUp (KeyCode.Escape)) {
			#if UNITY_WEBPLAYER
			Application.LoadLevel ("00_LOGOSCENE");
			#else
			Application.Quit ();
			#endif
		}
	}
}
