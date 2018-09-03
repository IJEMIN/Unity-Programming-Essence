using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 적 게임 오브젝트를 주기적으로 생성한다
public class EnemySpawner : MonoBehaviour {
    public float damageMax = 40f; // 최대 공격력
    public float damageMin = 20f; // 최소 공격력
    private List<Enemy> enemies = new List<Enemy>(); // 생성된 적들을 담는 리스트
    public Enemy enemyPrefab; // 생성할 적 AI

    public Text enemyWaveText; // 적 개수 표시 텍스트

    public float healthMax = 200f; // 최대 체력
    public float healthMin = 100f; // 최소 체력
    public Transform[] spawnPoints; // 적 AI를 소환할 위치들

    public float speedMax = 3f; // 최대 속도
    public float speedMin = 1f; // 최소 속도

    public Color strongEnemyColor = Color.red; // 강한 적 AI가 가지게 될 피부색
    public LivingEntity targetEntity; // 생성되는 적 AI들이 추적할 대상

    private int wave; // 현재 웨이브

    private void Update() {
        // 게임 오버 상태일때는 생성하지 않음
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            return;
        }

        // 적을 모두 물리친 경우 다음 스폰 실행
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

        wave++; // 웨이브 1 증가

        // 현재 웨이브 * 1.5에 반올림 한 개수 만큼 적 AI를 생성한다
        var spawnCount = Mathf.RoundToInt(wave * 1.5f);

        for (var i = 0; i < spawnCount; i++)
        {
            var enemyIntensity = Random.Range(0f, 1f);
            CreateEnemy(enemyIntensity);
        }
    }

    private void CreateEnemy(float intensity) {
        // 적을 생성하고 생성한 적에게 추적할 대상을 할당

        // intensity를 기반으로 적의 능력치 결정
        var health = Mathf.Lerp(healthMin, healthMax, intensity);
        var damage = Mathf.Lerp(damageMin, damageMax, intensity);
        var speed = Mathf.Lerp(speedMin, speedMax, intensity);

        // intensity를 기반으로 하얀색과 enemyStrength 사이에서 적의 피부색 결정
        var skinColor = Color.Lerp(Color.white, strongEnemyColor, intensity);

        // 생성할 위치를 랜덤으로 결정
        var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // 적 프리팹으로부터 적 생성
        var enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // 생성한 적의 능력치와 추적 대상 설정
        enemy.Setup(health, damage, speed, skinColor, targetEntity);

        enemies.Add(enemy); // 생성된 적을 리스트에 추가

        // 적의 onDeath 이벤트에 익명 메서드 등록
        enemy.onDeath += () => enemies.Remove(enemy); // 사망한 적을 리스트에서 제거
        enemy.onDeath += () => GameManager.instance.AddScore(100); // 적 사망시 점수 상승
    }
}