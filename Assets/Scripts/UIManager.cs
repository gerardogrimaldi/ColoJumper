using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	[SerializeField]
	ScoreManager scoreManager;

	[SerializeField]
	Text gameOverText;

	[SerializeField]
	Text scoreText;

	[SerializeField]
	GameObject mainMenuPanel;

	[SerializeField]
	GameObject highScorePanel;

	[SerializeField]
	Text[] highScores;

	public void ShowScoreText(int _score){

		scoreText.text = "Score: " + _score.ToString ();

	}

	public void GameEnd(bool showGOver){

		mainMenuPanel.SetActive (true);
		if(showGOver)
		gameOverText.gameObject.SetActive(true);

	}

	public void GameStart(){

		scoreText.gameObject.SetActive (true);
		mainMenuPanel.SetActive (false);
		gameOverText.gameObject.SetActive (false);

	}

	public void Show(GameObject toShow){

		toShow.SetActive (true);

	}

	public void Hide(GameObject toHide){

		toHide.SetActive (false);

	}

	public void ShowHighScore(){

		int[] scores = scoreManager.dapatkanScores ();

		for (int i = 0; i < highScores.Length; i++) {
			if (i < scores.Length)
				highScores [i].text = scores [i].ToString();
			else
				highScores [i].text = "0";
		}
		highScorePanel.SetActive (true);
	}

}
