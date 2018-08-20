using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.WebCam;

// 적 게임 오브젝트를 주기적으로 생성한다
public class EnemySpawner : MonoBehaviour {
    public Enemy enemyPrefab; // 생성할 적 AI
    public LivingEntity playerEntity; // 생성되는 적 AI들이 추적할 대상
    public Transform[] spawnPositions; // 적 AI를 소환할 위치들

    public Text enemyWaveText; // 적 개수 표시 텍스트
    private List<Enemy> enemies = new List<Enemy>(); // 생성된 적들을 담는 리스트

    private int wave = 0; // 현재 웨이브

    public Color strongEnemeyColor = Color.red; // 강한 적 AI가 가지게 될 피부색
    
    // 생성할 적 AI에 할당할 능력치들의 범위
    public float damageMax = 40f; // 최대 공격력
    public float damageMin = 20f; // 최소 공격력

    public float healthMax = 200f; // 최대 체력
    public float healthMin = 100f; // 최소 체력

    public float speedMax = 3f; // 최대 속도
    public float speedMin = 1f; // 최소 속도

    void Update() {
        // 게임 오버 상태일때는 생성하지 않음
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            return;
        }

        // 적을 모두 물리친 경우 다음 웨이브 실행
        if (enemies.Count <= 0)
        {
            SpawnWave();
        }

        UpdateUI(); // UI 갱신
    }

    private void UpdateUI() {
        enemyWaveText.text = "Wave : " + wave + "\n" + "Enemy Left : " + enemies.Count;
    }

    private void SpawnWave() {
        // 현재 웨이브 수에 맞춰 적을 생성한다
    }

    private void CreateEnemey(float powerPercentage) {
        // 입력된 파워 수치에 맞춰 적을 생성
    }
}