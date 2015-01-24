using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ObjectId : MonoBehaviour {
	public string id = "";

	#if UNITY_EDITOR
	void OnGUI () {
		if (id == "") {
			RefreshObjectId ();
		}
	}

	public void RefreshObjectId () {
		id = ObjectIdManager.GetNewObjectId();
	}
	#endif

	public string GetObjectId () {
		return id;
	}
}

