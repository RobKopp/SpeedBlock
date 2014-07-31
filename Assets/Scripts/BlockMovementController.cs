using UnityEngine;

public class BlockMovementController : MonoBehaviour {

	public delegate void OnLose();
	public delegate void OnWin();

	public OnLose loseCallback;
	public OnWin winCallback;

	Vector3 startingPosition;

	Vector3 movementDirection;

	public Vector3 StartingDirection;

	float speed;

	public float StartingSpeed;

	public float Acceleration;

	bool started;

	void Start() {
		transform.forward = StartingDirection.normalized;
		startingPosition = transform.position;
	}


	public void Reset() {
		transform.position = startingPosition;
		transform.forward = StartingDirection.normalized;
		speed = 0;
		started = false;
	}

	public Vector3 MovementDirection {
		get {
			return movementDirection;
		}
	}

	public float Speed {
		get {
			return speed;
		}
	}

	void StartEngines() {
		started = true;
		speed = StartingSpeed;
		SetDirection(StartingDirection.normalized);
	}

	public void SetDirection(Vector3 direction) {
		if(started) {
			movementDirection = direction.normalized;
			transform.forward = movementDirection;
		}
	}

	
	void Update () {
		if(started) {
			transform.position += movementDirection * (speed * Time.deltaTime);
			speed += Acceleration * Time.deltaTime;
		}
	}

	void Failed() {
		Stop ();
		loseCallback();
	}

	void Win() {
		Stop ();
		winCallback();
	}

	void Stop() {
		started = false;
		speed = 0;
	}
}
