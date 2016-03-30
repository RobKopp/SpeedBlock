using UnityEngine;
using System.Collections;

public class WarpPadController : MonoBehaviour {

	public GameObject Output;

	// Use this for initialization
	void OnTriggerEnter(Collider collider) {
		if(Output != null) {
			collider.transform.position = Output.transform.position;
		}
	}
}
