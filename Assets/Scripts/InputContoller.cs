using UnityEngine;
using System.Collections;

public class InputContoller : MonoBehaviour {

	Vector2 tapStart;

	public BlockMovementController block;
	public Swipe SwipeHandler;

	// Update is called once per frame
	void Start() {
		SwipeHandler.onMouseSwipe += OnSwipe;
	}

	void Update() {
#if !UNITY_IOS && !UNITY_ANDROID

		Vector3 desiredDirection = Vector3.zero;
		if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
			desiredDirection = Vector3.left;
		}
		if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
			desiredDirection = Vector3.up;
		}
		if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
			desiredDirection = Vector3.down;
		}
		if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow)) {
			desiredDirection = Vector3.right;
		}
		if(desiredDirection != Vector3.zero && desiredDirection != block.MovementDirection && desiredDirection != (-1 * block.MovementDirection)) {
			block.SetDirection(desiredDirection);
		}
#endif
	}

//	void OnLevelWasLoaded(int levelNum) {
//		block = GameObject.FindGameObjectWithTag("Player").GetComponent<BlockMovementController>();
//	}

	void OnSwipe(Vector3 swipeDirection) {

		Vector3 desiredDirection;
		//Check which direction the swipe is going
		if(Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y)) {
			desiredDirection = Vector3.left * (swipeDirection.x < 0 ? -1 : 1);
		} else {
			desiredDirection = Vector3.up * (swipeDirection.y < 0 ? 1 : -1);
		}

		if(desiredDirection != block.MovementDirection && desiredDirection != (-1 * block.MovementDirection)) {
			block.SetDirection(desiredDirection);
		}
	}
}
