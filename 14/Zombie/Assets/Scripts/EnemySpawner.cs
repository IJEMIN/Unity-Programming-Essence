using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public Enemy enemyPrefab;
    public Transform[] spawnPositions;

    public Color strongEnemeyColor = Color.red;

    public float healthMax = 200;
    public float healthMin = 100;

    public float damageMin = 20;
    public float damageMax = 50;

    public float speedMin = 1f;
    public float speedMax = 3f;

    public LivingEntity playerEntity;

    public float timeBetSpawn = 5f;

    private int waveCount = 0;

    private int minSpawnCount = 1;

    void Start()
    {
        InvokeRepeating("Spawn", 1f, timeBetSpawn);
    }

    void Spawn()
    {
        if (!playerEntity)
        {
            return;
        }

        waveCount++;

        int spawnCount = minSpawnCount * waveCount * 2;


        for (int i = 0; i < minSpawnCount; i++)
        {
            float enemyStrength = Random.Range(0f, 1f);

            float health = Mathf.Lerp(healthMin, healthMax, enemyStrength);
            float damage = Mathf.Lerp(damageMin, damageMax, enemyStrength);
            float speed = Mathf.Lerp(speedMin, speedMax, enemyStrength);

            Color skinColor = Color.Lerp(Color.white, strongEnemeyColor, enemyStrength);

            int randomSel = Random.Range(0, spawnPositions.Length);
            Enemy createdEnemey = Instantiate(enemyPrefab, spawnPositions[randomSel].position, spawnPositions[randomSel].rotation);
            createdEnemey.Setup(health, damage, speed, skinColor, playerEntity);
        }


    }

}