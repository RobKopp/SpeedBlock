using UnityEngine;
using System.Collections.Generic;
using MiniJSON;

public class BlockMovementController : MonoBehaviour {

	public delegate void OnLose();
	public delegate void OnWin();

	public OnLose loseCallback;
	public OnWin winCallback;

	Vector3 startingPosition;

	Vector3 movementDirection;

	public Vector3 StartingDirection;

	float speed;

	float StartingSpeed;

	public float Acceleration;

	bool started;

	void Start() {
		transform.right = StartingDirection.normalized;
		startingPosition = transform.position;
	}

	public void Reset() {
		transform.position = startingPosition;
		transform.right = StartingDirection.normalized;
		speed = 0;
		SetMeshHidden(false);
		if(particleSystem.isPlaying) {
			particleSystem.Stop();
		}
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
		SetDirection(StartingDirection.normalized);
	}
	
	public void SetDirection(Vector3 direction) {
		//The input controller checks if its a valid swipe
		if(started) {
			movementDirection = direction.normalized;
			transform.right = movementDirection;
			speed += Acceleration;
		}
	}

	
	void Update () {
		if(started) {
			transform.position += movementDirection * (speed * Time.deltaTime);
			//We don't use flat acceleration anymore, use turning to accelerate
//			speed += Acceleration * Time.deltaTime;
		}
	}

	void Failed() {
		Stop ();
		SetMeshHidden(true);
		particleSystem.Play();
		loseCallback();
	}

	void SetMeshHidden(bool hidden) {
		GameObject mesh = transform.FindChild("Mesh").gameObject;
		mesh.SetActive(!hidden);
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
