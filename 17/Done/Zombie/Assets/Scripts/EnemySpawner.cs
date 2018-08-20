using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.WebCam;

// 적 게임 오브젝트를 주기적으로 생성한다
public class EnemySpawner : MonoBehaviour {
    public Enemy enemyPrefab; // 생성할 적 AI
    public LivingEntity playerEntity; // 생성되는 적 AI들이 추적할 대상
    public Transform[] spawnPositions; // 적 AI를 소환할 위치들
    public Color strongEnemeyColor = Color.red; // 강한 적 AI가 가지게 될 피부색

    public Text enemyWaveText; // 적 개수 표시 텍스트
    private List<Enemy> enemies = new List<Enemy>(); // 생성된 적들을 담는 리스트

    private int wave = 0; // 현재 웨이브

    // 적 AI가 생성되면서 가지게 될 스펙들의 범위
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
        // 웨이브 1 증가
        wave++;

        // 현재 웨이브 * 1.5에 반올림 한 개수 만큼 적 AI를 생성한다
        int spawnCount = Mathf.CeilToInt(wave * 1.5f);

        for (int i = 0; i < spawnCount; i++)
        {
            //  적 AI의 스펙을 랜덤하게 결정한다
            float powerPercentage = Random.Range(0f, 1f);
            CreateEnemey(powerPercentage);
        }
    }

    private void CreateEnemey(float powerPercentage) {
        // 실제 적 AI를 생성하고, 적 AI의 강함을 최소(0)과 최대(1) 사이로 받는다

        // 체력과 공격력과 속도를 power를 통해 최소~최대 사이에서 결정한다
        float health = Mathf.Lerp(healthMin, healthMax, powerPercentage);
        float damage = Mathf.Lerp(damageMin, damageMax, powerPercentage);
        float speed = Mathf.Lerp(speedMin, speedMax, powerPercentage);

        // 사용할 컬러를 enemyStrength를 통해 원래색~붉은색 사이에서 결정한다
        Color skinColor = Color.Lerp(Color.white, strongEnemeyColor, powerPercentage);

        // 생성할 위치를 랜덤으로 결정한다
        Transform spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];

        // 새로운 적 AI 게임오브젝트를 생성한다
        Enemy enemy = Instantiate(enemyPrefab, spawnPosition.position, spawnPosition.rotation);

        // 위에서 랜덤 계산한 스펙들로 생성된 적 AI의 스펙을 설정하고, 추적할 대상을 설정해준다
        enemy.Setup(health, damage, speed, skinColor, playerEntity);

        // 적의 onDeath 이벤트에 메서드 등록
        enemy.onDeath += () => enemies.Remove(enemy); // 사망한 적을 리스트에서 제거
        enemy.onDeath += () => GameManager.instance.AddScore(100); // 적 사망시 점수 상승

        enemies.Add(enemy); // 생성된 적을 리스트에 추가
    }
}