using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager instance; // 싱글톤을 구현하기 위한 전역변수

    public bool isGameover = false; // 게임 오버인 상태인가?
    public Text scoreText; // 점수를 출력할 UI텍스트
    public GameObject gameoverUI; // 게임 오버시 활성화할 UI 게임 오브젝트

    private int score = 0; // 현재 게임 점수

    // 게임이 시작되었을때 싱글톤 변수를 설정한다
    void Awake () {
        // 싱글톤 변수 instance가 비어있는가?
        if (instance == null) {
            // instance가 비어있다면(null 이라면) 그곳에 자기 자신을 할당한다
            // 따라서 instance를 통해 누구나 나에게 접근할 수 있다.
            instance = this;

        } else // 만약 instance에 이미 어떤 GameManager 오브젝트가 할당되어 있다면
        {
            // 두개 이상의 GameManager 오브젝트가 씬에 존재한다는 의미이며
            // instance에 할당된 GameManager가 자신이 아니므로
            // 자신의 게임 오브젝트를 파괴한다
            Debug.LogWarning ("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy (gameObject);
        }
    }

    void Update () {

    }

    public void AddScore (int newScore) {

    }

    public void OnPlayerDead () {

    }
}