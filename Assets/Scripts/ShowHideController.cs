using UnityEngine;
using System.Collections;

public class ShowHideController : MonoBehaviour {

	public AnimationClip ShowAnimation;

	public AnimationClip HideAnimation;

	// Use this for initialization
	void Show () {
		GetComponent<Animation>().Play(ShowAnimation.name);
	}
	
	// Update is called once per frame
	void Hide () {
		GetComponent<Animation>().Play(HideAnimation.name);
	}
}
