using UnityEngine;
using System.Collections;

public class Swipe : MonoBehaviour {

	Vector3 swipeStart;

	public delegate void OnSwipe(Vector3 swipeDirection); 

	public OnSwipe onMouseSwipe;


	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			swipeStart = Input.mousePosition;
		}

		if(Input.GetMouseButtonUp(0)) {
			Vector3 currentMousePos = Input.mousePosition;
			onMouseSwipe(swipeStart - currentMousePos);
		}
	
	}
}
