using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	[SerializeField]
	UIManager uiManagerScript;

	[SerializeField]
	ScoreManager scoreManager;

	[SerializeField]
	PlatformSpawner platformSpawnerScript;

	[SerializeField]
	CameraFollow cameraScript;

	public PlayerController playerScript;

	public bool gameRunning = false;


	void Update(){

		if(gameRunning)
			uiManagerScript.ShowScoreText (playerScript.score);

	}

	public void GameOver(int score){

		scoreManager.AddScore (score);
		gameRunning = false;
		cameraScript.ResetCam ();
		playerScript.Reset ();
		platformSpawnerScript.ClearSpawnedPlatform ();
		platformSpawnerScript.enabled = false;

	}

	public void UIRestart(bool showGO){

		uiManagerScript.GameEnd(showGO);
	}

	public void StartGame(){

		platformSpawnerScript.enabled = true;
		uiManagerScript.GameStart ();
		playerScript.score = 0;
		playerScript.isDead = false;
		gameRunning = true;

	}

	public void QuitGame(){

		Application.Quit();

	}
}
