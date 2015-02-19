using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LevelRoot))]
public class LevelRootEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		LevelRoot levelRoot = (LevelRoot)target;
		if(GUILayout.Button("Export Level"))
		{
			levelRoot.Serialize();
		}

		if(GUILayout.Button ("Load Level")) {
			levelRoot.LoadLevel("");
		}
	}

}
