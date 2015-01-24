using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour {

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
		timer -= Time.deltaTime;
		if (timer < 0.0f) {
			stringId += 1;
			timer = timePerText;
			GetComponent<Text>().text = texte[stringId];
		}
	}
}
