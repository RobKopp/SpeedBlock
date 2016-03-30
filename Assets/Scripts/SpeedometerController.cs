using UnityEngine;
using System.Collections.Generic;

public class SpeedometerController : MonoBehaviour,ISerializable {

	public BlockMovementController blockMovementController;

	void Start() {
		blockMovementController = GameObject.FindGameObjectWithTag("Orig").GetComponent<BlockMovementController>();
	}

	public Dictionary<string,string> Serialize() {
		return new Dictionary<string,string>();
	}

	public void DeSerialize(Dictionary<string,object> options) {
		GameObject block = GameObject.FindGameObjectWithTag("Player");
		blockMovementController = block.GetComponent<BlockMovementController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(blockMovementController != null) {
			GetComponent<TextMesh>().text = (int)blockMovementController.Speed + " mph";
		}
	}
}
