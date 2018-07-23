using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public Enemy enemyPrefab;
    public LivingEntity playerEntity;
    public Transform[] spawnPositions;
    public Color strongEnemeyColor = Color.red;

    public float damageMax = 40f;
    public float damageMin = 20f;

    public float healthMax = 200f;
    public float healthMin = 100f;

    public float speedMax = 3f;
    public float speedMin = 1f;
    public float timeBetSpawn = 7f;

    private int wave = 0;

    private void Start () {
        InvokeRepeating ("Spawn", 1f, timeBetSpawn);
    }

    private void Spawn () {
        if (!playerEntity) {
            return;
        }

        wave++;
        int spawnCount = Mathf.CeilToInt(wave * 1.5f);

        for (int i = 0; i < spawnCount; i++) {
            CreateEnemey ();
        }
    }

    void CreateEnemey () {

        float enemyStrength = Random.Range (0f, 1f);

        float health = Mathf.Lerp (healthMin, healthMax, enemyStrength);
        float damage = Mathf.Lerp (damageMin, damageMax, enemyStrength);
        float speed = Mathf.Lerp (speedMin, speedMax, enemyStrength);

        Color skinColor = Color.Lerp (Color.white, strongEnemeyColor, enemyStrength);

        Transform spawnPosition = spawnPositions[Random.Range (0, spawnPositions.Length)];

        Enemy enemyInstance = Instantiate (enemyPrefab, spawnPosition.position, spawnPosition.rotation);
        enemyInstance.Setup (health, damage, speed, skinColor, playerEntity);
    }
}