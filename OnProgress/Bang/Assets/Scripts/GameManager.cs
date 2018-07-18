using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public Text scoreText;
	public GameObject gameoverUI;

	public AudioSource effectAudioPlayer;
	private int score = 0;

	public static GameManager instance {
		get {
			if (m_instance == null) {
				m_instance = FindObjectOfType<GameManager> ();
			}
			return m_instance;
		}
	}

	private static GameManager m_instance;

	void Awake () {
		if (instance != this) {
			Destroy (gameObject);
		}
	}

	public void AddScore (int newScore) {
		score += newScore;
		UpdateUI ();
	}

	void UpdateUI () {
		scoreText.text = "SCORE : " + score;
	}

	public void PlaySoundEffect (AudioClip clip) {
		effectAudioPlayer.PlayOneShot (clip);
	}

}