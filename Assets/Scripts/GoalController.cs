using UnityEngine;
using System.Collections;

public class GoalController : MonoBehaviour {

	public float SpeedLimit;

	public TextMesh SpeedLimitSign;

	void Start() {
		SpeedLimitSign.text = ((int)SpeedLimit) + " mph";
	}

	// Use this for initialization
	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.GetComponent<BlockMovementController>().Speed >= SpeedLimit) {
			collider.SendMessage("Win");
		} else {
			collider.SendMessage("Failed");
		}
	}
}
