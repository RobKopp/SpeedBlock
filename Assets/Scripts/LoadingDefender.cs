using UnityEngine;
using System.Collections;

public class LoadingDefender : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Object.DontDestroyOnLoad(gameObject);
	}
}
