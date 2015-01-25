using UnityEngine;
using System.Collections;

public class WinningCondition : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other) {
		RepairConsole rc = FindObjectOfType<RepairConsole>();
		if (rc != null) {
			if (rc.repaired) {
				Application.LoadLevel("100_ExtroSequence");
			}
		}
	}
}
