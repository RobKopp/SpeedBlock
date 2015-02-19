using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoalController : MonoBehaviour, ISerializable {

	public float SpeedLimit;

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.gameObject.GetComponent<BlockMovementController>().Speed >= SpeedLimit) {
			collider.SendMessage("Win");
		} else {
			collider.SendMessage("Failed");
		}
	}

	public Dictionary<string,string> Serialize() {
		Dictionary<string,string> options = new Dictionary<string, string>();
		options.Add ("SpeedLimit", SpeedLimit.ToString());
		return options;
	}

	public void DeSerialize(Dictionary<string,object> options) {
		SpeedLimit = float.Parse((string)options["SpeedLimit"]);
	}
}
