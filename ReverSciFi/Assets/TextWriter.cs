using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour {

	public Camera mainCamera;
	//public Camera textCamera;
	public GameObject character;

	private string[] texte = new string[] {
		"What a fight!",
		
		"I feel exhausted.",
		
		"I have to get Ms. Scottocs, urgently!",
		
		"Are we crashing?",
		
		"Quickly, now!",
		
		"Frack, that was close.",
		
		"Less than 20 Minuten to go.",
		
		"Jesses Maria!",
		
		"What do we do now?"
	};
	int stringId = -1;

	private float timePerText = 2.5f;
	float timer = 0.0f;
	public bool isPlaying = false;

	void Start () {
		timer = timePerText;
	}

	// Update is called once per frame
	void Update () {
		/*Vector3 charScreenPos = mainCamera.WorldToScreenPoint(character.transform.position);
		Vector2 textScreenPos = GUIUtility.ScreenToGUIPoint(new Vector2(charScreenPos.x, charScreenPos.y));
		transform.position = textScreenPos;*/
		//transform.parent.GetComponent<Canvas>().pixelRect = new Rect(transform.position.x, transform.position.y, 200, 200);
		if (transform.lossyScale.x < 0) {
			transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
		timer -= Time.deltaTime;
		if (timer < 0.0f) {
			stringId = Mathf.Min (stringId + 1, texte.Length-1);
			timer = timePerText;
			GetComponent<Text>().text = texte[stringId];
		}
	}
}
