using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking;

// 점수와 게임 오버 여부, 게임 UI를 관리하는 게임 매니저
public class GameManager : NetworkBehaviour {
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

    private static GameManager m_instance; // 싱글톤이 할당될 static 변수

    [SyncVar] public int enemyCount;
    [SyncVar] public int wave;

    public GameObject gameoverUI; // 게임 오버시 활성화될 UI
    public Text scoreText; // 점수 표시 텍스트

    [SyncVar] private int score = 0; // 현재 게임 점수
    public bool isGameover { get; private set; } // 게임 오버 상태

    private void Awake() {
        // 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
    }

    void Update() {
        UIManager.instance.UpdateWaveText(wave, enemyCount);
    }

    // 게임 재시작
    public void Restart() {
        // 게임 오버인 상태에서만 게임 재시작 가능
        if (isGameover)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // 점수를 추가하고 UI 갱신
    public void AddScore(int newScore) {
        // 게임 오버가 아닌 상태에서만 점수 증가 가능
        if (!isGameover && isServer)
        {
            // 점수 추가
            score += newScore;
            // 점수 UI 텍스트 갱신
            scoreText.text = "SCORE : " + score;
        }
    }

    // 게임 오버 처리
    public void EndGame() {
        // 게임 오버 상태를 참으로 변경
        isGameover = true;
        // 게임 오버 UI를 활성화
        gameoverUI.SetActive(true);
    }
}