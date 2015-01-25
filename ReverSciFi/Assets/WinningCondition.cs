using UnityEngine;
using System.Collections;

public class WinningCondition : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other) {
		if (FindObjectOfType<RepairConsole>().repaired) {
			Application.LoadLevel("100_ExtroSequence");
		}
	}
}
