using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform FollowMe;

	public float FollowThreshold;

	public float Damping;

	public bool ShouldFollow;

	Vector3 StartingPosition;

	void Start() {
		StartingPosition = transform.position;
	}

	void Update() {
		if(ShouldFollow) {
			Vector3 followMePos = FollowMe.position;
			followMePos.y = transform.position.y;
			Vector3 diff = transform.position - followMePos;

			if(diff.magnitude > FollowThreshold) {
				Vector3 desiredPosition = (diff.normalized * FollowThreshold) - diff;
				transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * Damping);

			}
		}
	}

	void Reset() {
		transform.position = StartingPosition;
	}
}
