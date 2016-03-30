using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections;

public class GameStateManager : MonoBehaviour {

	public enum GameState {
		Transitioning,
		Started,
		WinState,
		LoseState,
		Loading,
		Stopped
	}

	public SceneLevelLoader levelLoader;
	public int StartingLevel;

	public GameObject Cover;

	int currentLevel;

	public GameState currentState;

	public GameObject currBlock;

	GameObject currTopLevel;

	float startTime;

	public void Start() {
		currentLevel = StartingLevel;
		currentState = GameState.Loading;
		currBlock.GetComponent<BlockMovementController>().winCallback += OnWin;
		currBlock.GetComponent<BlockMovementController>().loseCallback += OnLose;
		loadLevel(currentLevel);

	}

	void OnLevelWasLoaded(int levelNum) {
		GameObject block = GameObject.FindGameObjectWithTag("Spawn");
		currBlock.SendMessage("SetStart", block.transform);
		Destroy (block);

		currentState = GameState.Stopped;

		GameObject deleteNode = GameObject.FindGameObjectWithTag("Dead");
		GameObject.Destroy(deleteNode);

//		currTopLevel = GameObject.FindGameObjectWithTag("TopLevel");
//		currTopLevel.SetActive(false);
		Cover.SendMessage("Hide");
		currentState = GameState.Stopped;
	}

	void OnWin() {
		currentState = GameState.WinState;
		NextLevel();
	}
	
	void OnLose() { 
		currentState = GameState.LoseState;
	}

	public void NextLevel() {
		currentLevel += 1;
		Cover.SendMessage("Show");
		loadLevel(currentLevel);

	}

	void loadLevel(int levelToLoad) {
		levelLoader.LoadLevel(levelToLoad);
	}

	void Update() {
		#if !UNITY_EDITOR
		if(currentState == GameState.Stopped || currentState == GameState.LoseState) {
			if(Input.touchCount > 0) {
				foreach(Touch touch in Input.touches) {
					if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
						if(currentState == GameState.Stopped) {
							StartGame ();

						} else if(currentState == GameState.LoseState) {
							Reset ();
						}
						break;

					}
				}
			}
		}
		#else
		if(Input.GetMouseButtonDown(0)) {
			if(currentState == GameState.Stopped) {
				StartGame ();
			} else if(currentState == GameState.LoseState) {
				Reset ();
			}
		}
		#endif
	}

	void StartGame() {
		startTime = Time.time;

		currentState = GameState.Started;
		currBlock.SendMessage("StartEngines");
	}

	void Reset() {
		currBlock.SendMessage("Reset");
//		Camera.main.SendMessage("Reset");
		currentState = GameState.Stopped;
//		showGameStart = true;
	}


}
