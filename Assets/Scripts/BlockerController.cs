using UnityEngine;
using System.Collections;

public class BlockerController : MonoBehaviour {

	public GameObject ExplosionEffects;
	

	void OnCollisionEnter2D(Collision2D collision) {
		collision.gameObject.SendMessage("Failed");
	}
}
