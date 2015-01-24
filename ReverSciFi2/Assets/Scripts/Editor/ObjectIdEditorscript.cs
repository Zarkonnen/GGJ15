using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ObjectId))]
public class ObjectIdEditorscript : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		
		ObjectId myScript = (ObjectId)target;
		if(GUILayout.Button("ResetId's")) {
			ObjectIdManager.ResetAllIds();
		}
	}
}
