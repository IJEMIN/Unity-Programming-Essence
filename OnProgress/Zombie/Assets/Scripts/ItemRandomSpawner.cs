using UnityEngine;
using UnityEngine.AI;

// 주기적으로 아이템을 플레이어 근처에 생성한다.
// 생성할 위치는 플레이어를 기준으로 일정 반경 안에서 네브 메쉬 위의 랜덤한 지점을 찾아 설정한다.
public class ItemRandomSpawner : MonoBehaviour {
    public GameObject[] items; // 생성할 아이템들

    public Transform playerTransform; // 플레이어의 Transform

    private float timeBetSpawn; // 생성 간격
    public float timeBetSpawnMax = 7f; // 최대 시간 간격
    public float timeBetSpawnMin = 2f; // 최소 시간 간격
    private float lastSpawnTime; // 마지막으로 생성한 시간

    private void Start () {
        // 사용할 값들 초기화
        timeBetSpawn = Random.Range (timeBetSpawnMin, timeBetSpawnMax);
        lastSpawnTime = 0;
    }

    private void Update () {
        // 주기적으로 아이템을 생성한다
        if (Time.time >= lastSpawnTime + timeBetSpawn) {
            lastSpawnTime = Time.time; // 마지막 생성 시간을 갱신
            timeBetSpawn = Random.Range (timeBetSpawnMin, timeBetSpawnMax); // 생성 주기를 랜덤으로 변경
            Spawn (); // 실제 아이템을 생성
        }
    }

    private void Spawn () {
        // 아이템을 생성하는 처리가 온다

        if (GameManager.instance.isGameover) {
            // 게임 오버시 아이템을 생성하지 않음
            return;
        }

        // 플레이어 근처의 네브 메쉬위의 랜덤 위치를 가져온다
        Vector3 spawnPosition = GetRandomPositionOnNavMesh (playerTransform.position, 5f);
        spawnPosition += Vector3.up * 0.5f; // 바닥에서 0.5만큼 위로 올린다

        // 아이템 중 하나를 무작위로 골라 랜덤 위치에 생성한다
        Instantiate (items[Random.Range (0, items.Length)], spawnPosition, Quaternion.identity);
    }

    // 네브 메시 위의 랜덤한 위치를 반환하는 메서드
    // center를 중심으로 maxDistance 반경 안에서 랜덤한 위치를 찾는다
    private Vector3 GetRandomPositionOnNavMesh (Vector3 center, float maxDistance) {

        // center를 중심으로 반지름이 maxDinstance인 구 안에서의 랜덤한 위치 하나를 저장한다
        // Random.insideUnitSphere는 반지름이 1인 구 안에서의 랜덤한 한 점을 반환하는 프로퍼티
        Vector3 randomPos = Random.insideUnitSphere * maxDistance + center;

        NavMeshHit hit; // 네브 메시 샘플링의 정보를 저장하는 변수

        // randomPos를 기준으로 maxDistance 반경 안에서, randomPos에 가장 가까운 네브 메시 위의 한 점을 찾는다
        NavMesh.SamplePosition (randomPos, out hit, maxDistance, NavMesh.AllAreas);

        return hit.position; // 찾은 점을 반환
    }
}