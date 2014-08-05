using UnityEngine;
using System.Collections.Generic;

public class CameraStart : MonoBehaviour, ISerializable {

	public Dictionary<string,string> Serialize() {
		return new Dictionary<string,string>();
	}

	public void DeSerialize(Dictionary<string,object> options) {
		Camera.main.transform.position = transform.position;
		Camera.main.transform.localRotation = transform.localRotation;
	}
}
