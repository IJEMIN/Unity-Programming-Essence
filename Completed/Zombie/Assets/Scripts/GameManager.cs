using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityScript.Scripting.Pipeline;

// 점수와 게임 오버 여부, 게임 UI를 관리하는 게임 매니저
public class GameManager : MonoBehaviour {
    // 외부에서 싱글톤 오브젝트를 가져올때 사용할 프로퍼티
    public static GameManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<GameManager>();
            }

            return m_instance; // 싱글톤 오브젝트를 반환
        }
    }

    private static GameManager m_instance; // 싱글톤 오브젝트가 할당될 static 변수

    public GameObject gameoverUI; // 게임 오버시 활성화될 UI
    public Text scoreText; // 점수 표시용 텍스트 UI

    private int score; // 현재 게임 점수
    public bool isGameover { get; private set; } // 게임 오버를 표현하는 프로퍼티

    private void Awake() {
        // 이미 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        if (instance != this)
        {
            Destroy(gameObject); // 자신을 파괴
        }
    }

    void Start() {
        FindObjectOfType<PlayerHealth>().onDeath += Gameover;
    }

    public void Restart() {
        // 게임 오버인 상태라면 게임을 재시작한다

        if (isGameover)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void AddScore(int newScore) {
        // 점수를 추가하고 UI를 갱신한다

        if (!isGameover)
        {
            score += newScore; // 점수 추가
            scoreText.text = "SCORE : " + score; // 점수 UI 텍스트 갱신
        }
    }

    public void Gameover() {
        // 게임 오버를 위한 처리들이 온다

        isGameover = true; // 게임 오버 상태를 참으로 만든다
        gameoverUI.SetActive(true); // 게임 오버 UI를 활성화 한다
    }
}