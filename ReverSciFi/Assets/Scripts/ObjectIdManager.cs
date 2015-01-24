using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;

[ExecuteInEditMode]
public class ObjectIdManager : MonoBehaviour {

	static int id = 0;

	public static string GetNewObjectId () {
		id += 1;
		return EditorApplication.currentScene + id.ToString();
	}

	public static void ResetAllIds () {
		id = 0;
		ObjectId[] ids = Resources.FindObjectsOfTypeAll<ObjectId>();
		for (int i=0; i<ids.Length; i++) {
			ids[i].RefreshObjectId();
		}
	}

}
#endif