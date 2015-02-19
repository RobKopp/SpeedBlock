using UnityEngine;
using System.Collections.Generic;

public class SpeedLimitSignController : MonoBehaviour,ISerializable {

	public int SpeedLimit;

	void Start() {
		GameObject goal = GameObject.FindGameObjectWithTag("Goal");
		GetComponent<TextMesh>().text = goal.GetComponent<GoalController>().SpeedLimit + " mph";
	}

	public Dictionary<string,string> Serialize() {
		Dictionary<string,string> options = new Dictionary<string, string>();
		options.Add ("speedLimit", SpeedLimit.ToString());
		return options;
	}

	public void DeSerialize(Dictionary<string,object> options) {
		if(options.ContainsKey("speedLimit")) {
			SpeedLimit = int.Parse((string)options["speedLimit"]);
		}
	}

}
