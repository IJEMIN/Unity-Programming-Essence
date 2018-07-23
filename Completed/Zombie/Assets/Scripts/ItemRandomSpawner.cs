using UnityEngine;
using UnityEngine.AI;

public class ItemRandomSpawner : MonoBehaviour {
    public GameObject[] items;

    public Transform playerTransform;

    private float timeBetSpawn;
    public float timeBetSpawnMax = 7f;
    public float timeBetSpawnMin = 2f;
    private float lastSpawnTime;

    private void Start () {
        timeBetSpawn = Random.Range (timeBetSpawnMin, timeBetSpawnMax);
        lastSpawnTime = 0;
    }

    private void Update () {
        if (Time.time >= lastSpawnTime + timeBetSpawn) {
            lastSpawnTime = Time.time;
            timeBetSpawn = Random.Range (timeBetSpawnMin, timeBetSpawnMax);
            Spawn ();
        }
    }

    private void Spawn () {
        Vector3 spawnPosition = GetRandomPositionOnNavMesh (playerTransform.position, 5f) + Vector3.up * 0.5f;

        Instantiate (items[Random.Range (0, items.Length)], spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomPositionOnNavMesh (Vector3 center, float maxDistance) {
        Vector3 randomPos = Random.insideUnitSphere * maxDistance + center;

        NavMeshHit hit;
        NavMesh.SamplePosition (randomPos, out hit, maxDistance, NavMesh.AllAreas);

        return hit.position;
    }
}