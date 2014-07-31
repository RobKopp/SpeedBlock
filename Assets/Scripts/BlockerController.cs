using UnityEngine;
using System.Collections;

public class BlockerController : MonoBehaviour {

	public GameObject ExplosionEffects;
	

	void OnTriggerEnter(Collider collider) {
		collider.SendMessage("Failed");
	}
}
