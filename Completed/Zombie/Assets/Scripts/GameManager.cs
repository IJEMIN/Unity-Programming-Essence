using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private static GameManager m_instance;
    public static GameManager instance {
        get {
            if (m_instance == null) {
                m_instance = FindObjectOfType<GameManager> ();
            }
            return m_instance;
        }
    }

    public GameObject gameoverUI;
    public Text scoreText;

    private int score;
    public bool isGameover { get; private set; }

    private void Awake () {
        if (instance != this) {
            Destroy (gameObject);
        }
    }

    public void Restart () {
        if (isGameover) {
            SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
        }
    }

    public void AddScore (int newScore) {
        if (!isGameover) {
            score += newScore;
            UpdateUI ();
        }
    }

    private void UpdateUI () {
        scoreText.text = "SCORE : " + score;
    }
    public void Gameover () {
        isGameover = true;
        gameoverUI.SetActive (true);
    }
}