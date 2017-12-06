using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public void AddScore(int scoreToAdd){

	int indeksSkorYangKosong = DapatSkorIndeksYangKosong ();
	if (indeksSkorYangKosong < 5)
		PlayerPrefs.SetInt ("Score" + indeksSkorYangKosong, scoreToAdd);
	else {
		if (scoreToAdd > DapatkanSkorPalingKecil ()) {
			PlayerPrefs.SetInt ("Score" + DapatkanIndeksSkorPalingKecil (), scoreToAdd);		
		}
	PlayerPrefs.Save ();
	}
	}

	int DapatSkorIndeksYangKosong(){

		for (int i = 0; i < 5; i++) {
			if (!PlayerPrefs.HasKey ("Score" + i))
				return i;
		}
		return 5;
	}

	int DapatkanSkorPalingKecil(){
		int skor = PlayerPrefs.GetInt ("Score0");
		for (int i = 1; i < 5; i++) {
			skor = Mathf.Min(skor,PlayerPrefs.GetInt("Score"+i)); 
		}
		return skor;
	}

	int DapatkanIndeksSkorPalingKecil(){
		int skorPalingKecil = DapatkanSkorPalingKecil ();
		int indeks = 0;
		for (int i = 0; i < 5; i++) {
			if (PlayerPrefs.GetInt ("Score" + i) == skorPalingKecil) {
				indeks = i;
				break;
			}
		}

		return indeks;

	}

	public int[] dapatkanScores(){
		int[] scores = new int[5];
		for (int i = 0; i < DapatSkorIndeksYangKosong(); i++) {
			scores [i] = PlayerPrefs.GetInt ("Score" + i);
		}
		System.Array.Sort (scores);
		System.Array.Reverse (scores);
		return scores;

	}

}
