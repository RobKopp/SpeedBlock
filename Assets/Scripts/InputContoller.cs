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

	void OnSwipe(Vector3 swipeDirection) {

		Vector3 desiredDirection;
		//Check which direction the swipe is going
		if(Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y)) {
			desiredDirection = Vector3.left * (swipeDirection.x < 0 ? -1 : 1);
		} else {
			desiredDirection = Vector3.forward * (swipeDirection.y < 0 ? 1 : -1);
		}

		if(desiredDirection != block.MovementDirection && desiredDirection != (-1 * block.MovementDirection)) {
			block.SetDirection(desiredDirection);
		}
	}
}
