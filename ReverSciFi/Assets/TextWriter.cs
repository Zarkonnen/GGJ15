using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour {

	public Camera mainCamera;
	//public Camera textCamera;
	public GameObject character;

	public string[] texte = new string[] {
		"@&!¥#@@!!",
		"Jesses Marie!",
		"Dark becomes my sight and view...",
		"Frack!",
		"Oh God!",
		"Hellfire!",
		"Faster, faster!",
		"Noooooo.....!",
		"Aaaaaaargh!",
		"Dissolve, you silly son of monsterfracker!",
		"Motherfrack!",
		"Down we go...",
		"Ooops!",
		"Come quickly, Mr. Scottocs, I need your help on the bridge! We're going down if you don't fix it fast. Damn monsterfrackers!"
	};
	int stringId = -1;

	public float timePerText = 5.0f;
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
			stringId += 1;
			timer = timePerText;
			GetComponent<Text>().text = texte[stringId];
		}
	}
}
