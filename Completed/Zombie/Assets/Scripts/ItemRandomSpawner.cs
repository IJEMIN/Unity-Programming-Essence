using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemRandomSpawner : MonoBehaviour {
    public Transform playerTransform;
    public GameObject[] items;

    public float timeBetSpawnMin = 2f;
    public float timeBetSpawnMax = 7f;

    private float timeBetSpawn;

    private float lastSpawnTime;

    void Start() {
        timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
        lastSpawnTime = 0;
    }

    private void Update() {
        if (Time.time >= lastSpawnTime + timeBetSpawn) {
            lastSpawnTime = Time.time;
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            Spawn();
        }
    }

    void Spawn() {
        Vector3 randomPos = GetRandomPositionOnNavMesh(playerTransform.position, 5f);

        Vector3 spawnPosition = randomPos + Vector3.up * 0.5f;

        Instantiate(items[Random.Range(0, items.Length)], spawnPosition, Quaternion.identity);
    }

    Vector3 GetRandomPositionOnNavMesh(Vector3 center, float maxDistance) {
        Vector3 randomPos = Random.insideUnitSphere * maxDistance + center;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomPos, out hit, maxDistance, NavMesh.AllAreas);

        return hit.position;
    }
}