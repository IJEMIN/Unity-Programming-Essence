using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public Text scoreText;
	public GameObject gameoverUI;
	private int score = 0;
	public void AddScore (int newScore) {
		score += newScore;
		UpdateUI ();
	}

	void UpdateUI () {
		scoreText.text = "SCORE : " + score;
	}

}