using UnityEngine;
using System.Collections.Generic;

public class PlayerStartDefinition : MonoBehaviour,ISerializable {

	public float StartingSpeed;
	public float Acceleration;

	public Dictionary<string,string> Serialize () {
		Dictionary<string,string> options = new Dictionary<string, string>();
		options.Add ("StartingSpeed",StartingSpeed.ToString());
		options.Add ("Acceleration", Acceleration.ToString());

		return options;
	}

	public void DeSerialize (Dictionary<string,object> options) {
		StartingSpeed = float.Parse((string)options["StartingSpeed"]);
		Acceleration = float.Parse ((string)options["Acceleration"]);
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.transform.position = transform.position;
		player.transform.rotation = transform.rotation;

		BlockMovementController cont = player.GetComponent<BlockMovementController>();
		cont.Acceleration = Acceleration;
//		cont.StartingSpeed = StartingSpeed;
		cont.StartingDirection = transform.rotation.eulerAngles;
	}
}
