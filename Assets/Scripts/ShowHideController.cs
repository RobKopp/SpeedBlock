using UnityEngine;
using System.Collections;

public class ShowHideController : MonoBehaviour {

	public AnimationClip ShowAnimation;

	public AnimationClip HideAnimation;

	// Use this for initialization
	void Show () {
		animation.Play(ShowAnimation.name);
	}
	
	// Update is called once per frame
	void Hide () {
		animation.Play(HideAnimation.name);
	}
}
