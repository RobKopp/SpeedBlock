using UnityEngine;
using System.Collections;

public class BlockerController : MonoBehaviour {

	public GameObject ExplosionEffects;
	

	void OnCollisionEnter(Collision collision) {
		Debug.Log ("Expplode!");

		collision.gameObject.SendMessage("Failed", SendMessageOptions.DontRequireReceiver);
	}
}
