using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 적 게임 오브젝트를 주기적으로 생성한다
public class EnemySpawner : MonoBehaviour {
    public Enemy enemyPrefab; // 생성할 적 AI
    public LivingEntity playerEntity; // 생성되는 적 AI들이 추적할 대상
    public Transform[] spawnPositions; // 적 AI를 소환할 위치들
    public Color strongEnemeyColor = Color.red; // 강한 적 AI가 가지게 될 피부색


    public Text enemyCountText;
    private List<Enemy> enemies = new List<Enemy>();

    // 적 AI가 생성되면서 가지게 될 스펙들의 범위
    public float damageMax = 40f; // 최대 공격력
    public float damageMin = 20f; // 최소 공격력

    public float healthMax = 200f; // 최대 체력
    public float healthMin = 100f; // 최소 체력

    public float speedMax = 3f; // 최대 속도
    public float speedMin = 1f; // 최소 속도
    public float timeBetSpawn = 7f; // 생성 주기

    private int wave = 0; // 현재 웨이브 횟수

    private void Start() {
        // 1초 뒤에 Spawn 메서드를 timeBetSpawn 간격으로 주기적으로 반복 실행 시작
        InvokeRepeating("Spawn", 1f, timeBetSpawn);
    }

    private void Spawn() {
        // 현재 웨이브 수에 맞춰 적을 생성한다

        if (!playerEntity)
        {
            // 생성된 AI들이 추적할 대상이 없다면 적 AI를 생성하지 않는다
            return;
        }

        // 웨이브 1 증가
        wave++;

        // 현재 웨이브 * 1.5에 반올림 한 개수 만큼 적 AI를 생성한다
        int spawnCount = Mathf.CeilToInt(wave * 1.5f);

        for (int i = 0; i < spawnCount; i++)
        {
            CreateEnemey();
        }
    }

    void CreateEnemey() {
        // 실제 적 AI를 생성하고, 적 AI의 스펙을 랜덤하게 결정한다
    }

    private void UpdateUI() {
        enemyCountText.text = "ENEMY : " + enemies.Count;
    }
}