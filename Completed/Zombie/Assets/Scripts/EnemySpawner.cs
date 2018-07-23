using UnityEngine;

// 적 게임 오브젝트를 주기적으로 생성한다
public class EnemySpawner : MonoBehaviour {

    public Enemy enemyPrefab; // 생성할 적 AI
    public LivingEntity playerEntity; // 생성되는 적 AI들이 추적할 대상
    public Transform[] spawnPositions; // 적 AI를 소환할 위치들
    public Color strongEnemeyColor = Color.red; // 강한 적 AI가 가지게 될 피부색

    // 적 AI가 생성되면서 가지게 될 스펙들의 범위
    public float damageMax = 40f; // 최대 공격력
    public float damageMin = 20f; // 최소 공격력

    public float healthMax = 200f; // 최대 체력
    public float healthMin = 100f; // 최소 체력

    public float speedMax = 3f; // 최대 속도
    public float speedMin = 1f; // 최소 속도
    public float timeBetSpawn = 7f; // 생성 주기

    private int wave = 0; // 현재 웨이브 횟수

    private void Start () {
        // 1초 뒤에 Spawn 메서드를 timeBetSpawn 간격으로 주기적으로 반복 실행 시작
        InvokeRepeating ("Spawn", 1f, timeBetSpawn);
    }

    private void Spawn () {
        // 현재 웨이브 수에 맞춰 적을 생성한다

        if (!playerEntity) {
            // 생성된 AI들이 추적할 대상이 없다면 적 AI를 생성하지 않는다
            return;
        }

        // 웨이브 1 증가
        wave++;

        // 현재 웨이브 * 1.5에 반올림 한 개수 만큼 적 AI를 생성한다
        int spawnCount = Mathf.CeilToInt (wave * 1.5f);

        for (int i = 0; i < spawnCount; i++) {
            CreateEnemey ();
        }
    }

    void CreateEnemey () {
        // 실제 적 AI를 생성하고, 적 AI의 스펙을 랜덤하게 결정한다

        // 적 AI의 강함을 최소(0)과 최대(1) 사이에서 랜덤하게 결정한다
        float enemyStrength = Random.Range (0f, 1f);

        // 체력과 공격력과 속도를 enemyStrength를 통해 최소~최대 사이에서 결정한다
        float health = Mathf.Lerp (healthMin, healthMax, enemyStrength);
        float damage = Mathf.Lerp (damageMin, damageMax, enemyStrength);
        float speed = Mathf.Lerp (speedMin, speedMax, enemyStrength);

        // 사용할 컬러를 enemyStrength를 통해 원래색~붉은색 사이에서 결정한다
        Color skinColor = Color.Lerp (Color.white, strongEnemeyColor, enemyStrength);

        // 생성할 위치를 랜덤으로 결정한다
        Transform spawnPosition = spawnPositions[Random.Range (0, spawnPositions.Length)];

        // 새로운 적 AI 게임오브젝트를 생성한다
        Enemy enemyInstance = Instantiate (enemyPrefab, spawnPosition.position, spawnPosition.rotation);
        // 위에서 랜덤 계산한 스펙들로 생성된 적 AI의 스펙을 설정하고, 추적할 대상을 설정해준다
        enemyInstance.Setup (health, damage, speed, skinColor, playerEntity);
    }
}