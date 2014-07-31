using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {

	bool showGameStart = false;
	bool showWinScreen = false;
	bool showLoseScreen = false;

	public GameObject block;

	// Use this for initialization
	void Start () {
		showGameStart = true;
		BlockMovementController moveController = block.GetComponent<BlockMovementController>();
		moveController.winCallback += OnWin;
		moveController.loseCallback += OnLose;
	}

	void OnWin() {
		showWinScreen = true;
	}

	void OnLose() { 
		showLoseScreen = true;
	}
	
	void OnGUI() {
		Vector2 screenCenter = new Vector2(Screen.width/2,Screen.height/2);

		if(showGameStart) {
			Vector2 boxLoc = new Vector2(screenCenter.x - 250, screenCenter.y - 150);
			GUI.Box (new Rect(boxLoc.x,boxLoc.y,500,300), "Start Game");

			GUI.Label(new Rect(boxLoc.x + 100,boxLoc.y + 20,300,300), "Oh No! Due to a freak Paper Clipping accident Triangle Bot is out of control!  He's slowly speeding up and his only hope is to bust out through the blue doors!" +
				"Now is probably a bad time to mention this but we forgot to install the Turney Arounder Device so Triangle Bot won't be able to pull any U'ies anytime soon.  " +
				"\n\n -Swipe anywhere on this control device to tell Triangle Bot to go in that direction." +
				"\n -Avoid the obstacles long enough to build up the speed to break through the doors." +
			          "\n -Triangle Bot's life depends on you!");
			if(GUI.Button(new Rect(boxLoc.x + 212, boxLoc.y + 250, 96, 50), "I'm Ready!")) { 
				showGameStart = false;
				block.SendMessage("StartEngines");
			}
		}

		if(showWinScreen) {
			Vector2 boxLoc = new Vector2(screenCenter.x - 250, screenCenter.y - 150);
			GUI.Box (new Rect(boxLoc.x,boxLoc.y,500,300), "You Win!");
			
			GUI.Label(new Rect(boxLoc.x + 100,boxLoc.y + 20,300,300), "You made it out alive! Congratulations!");
			if(GUI.Button(new Rect(boxLoc.x + 212, boxLoc.y + 250, 96, 50), "Thanks!")) { 
				showWinScreen = false;
				Reset();

			}
		}

		if(showLoseScreen) {
			Vector2 boxLoc = new Vector2(screenCenter.x - 250, screenCenter.y - 150);
			GUI.Box (new Rect(boxLoc.x,boxLoc.y,500,300), "Bad Luck");
			
			GUI.Label(new Rect(boxLoc.x + 100,boxLoc.y + 20,300,300), "Oh No! You lost control and Triangle Bot crashed into a wall! Hey, at least we have a million of these things in the back...but we'll never get that one back.");
			if(GUI.Button(new Rect(boxLoc.x + 212, boxLoc.y + 250, 96, 50), "I'll Try Again!")) { 
				showLoseScreen = false;
				Reset ();
			}
		}

	}

	void Reset() {
		block.SendMessage("Reset");
		showGameStart = true;
	}
}
