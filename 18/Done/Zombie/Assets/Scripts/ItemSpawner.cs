using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

// 주기적으로 아이템을 플레이어 근처에 생성합니다.
// 생성할 위치는 플레이어를 기준으로 일정 반경 안에서 네브 메쉬 위의 랜덤한 지점을 찾아 설정합니다.
public class ItemSpawner : NetworkBehaviour {
    public GameObject[] items; // 생성할 아이템들

    public float maxDistance = 5f; // 플레이어 위치로부터 아이템이 배치될 최대 반경

    public float timeBetSpawnMax = 7f; // 최대 시간 간격
    public float timeBetSpawnMin = 2f; // 최소 시간 간격

    private float timeBetSpawn; // 생성 간격
    private float lastSpawnTime; // 마지막으로 생성한 시간

    private void Start() {
        // 생성 간격과 생성 시점 초기화
        timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
        lastSpawnTime = 0;
    }

    private void Update() {
        // 주기적으로 아이템 생성
        if (Time.time >= lastSpawnTime + timeBetSpawn && PlayerController.players.Count > 0)
        {
            lastSpawnTime = Time.time; // 마지막 생성 시간 갱신
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax); // 생성 주기를 랜덤으로 변경
            Spawn(); // 실제 아이템 생성
        }
    }

    private void Spawn() {
        // 아이템을 생성하는 처리
        int randomSel = Random.Range(0, PlayerController.players.Count);
        Transform targetTransform = PlayerController.players[randomSel].transform;

        // 플레이어 근처의 네브 메쉬위의 랜덤 위치를 가져옵니다.
        Vector3 spawnPosition = GetRandomPointOnNavMesh(targetTransform.position, maxDistance);
        spawnPosition += Vector3.up * 0.5f; // 바닥에서 0.5만큼 위로 올립니다.

        // 아이템 중 하나를 무작위로 골라 랜덤 위치에 생성합니다.
        GameObject item = Instantiate(items[Random.Range(0, items.Length)], spawnPosition, Quaternion.identity);

        NetworkServer.Spawn(item);
        // 생성된 아이템을 5초 뒤에 파괴
        Destroy(item, 5f);
    }

    // 네브 메시 위의 랜덤한 위치를 반환하는 메서드
    // center를 중심으로 distance 반경 안에서 랜덤한 위치를 찾습니다.
    private Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance) {
        // center를 중심으로 반지름이 maxDinstance인 구 안에서의 랜덤한 위치 하나를 저장합니다.
        // Random.insideUnitSphere는 반지름이 1인 구 안에서의 랜덤한 한 점을 반환하는 프로퍼티
        Vector3 randomPos = Random.insideUnitSphere * distance + center;

        NavMeshHit hit; // 네브 메시 샘플링의 정보를 저장하는 변수

        // randomPos를 기준으로 maxDistance 반경 안에서, randomPos에 가장 가까운 네브 메시 위의 한 점을 찾습니다.
        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);

        return hit.position; // 찾은 점 반환
    }
}