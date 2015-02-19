using UnityEngine;
using System.Collections;

public class SceneLevelLoader : MonoBehaviour {

	// Use this for initialization
	public void LoadLevel(int levelNum) {
		Application.LoadLevel("Level"+levelNum);
	}
}
