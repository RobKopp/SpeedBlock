using UnityEngine;
using System.Collections;

public class SpeedometerController : MonoBehaviour {

	public BlockMovementController block;
	
	// Update is called once per frame
	void Update () {
		GetComponent<TextMesh>().text = (int)block.Speed + " mph";
	}
}
