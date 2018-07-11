using System.Collections;
using UnityEngine;

// 바닥을 주기적으로 배치하는 스크립트
// 미리 바닥을 생성한 다음, 만들어진 바닥들을 돌아가며 사용하는 방식으로 무한 배치한다.
public class PlatformSpawner : MonoBehaviour {

    public GameObject platformPrefab; // 생성할 바닥의 원본 프리팹
    public int count = 3; // 게임이 시작될때 준비할 바닥 오브젝트의 갯수

    public float timeBetSpawnMin = 1.25f; // 다음번 스폰까지의 시간 간격의 최솟값
    public float timeBetSpawnMax = 2.25f; // 다음번 스폰까지의 시간 간격의 최댓값
    private float timeBetSpawn; // 다음번 스폰까지의 시간 간격

    public float yMin = -3.5f; // 스폰할 위치의 최소 y값
    public float yMax = 1.5f; // 스폰할 위치의 최대 y값
    private float xPos = 20f; // 스폰할 위치의 x 값

    private GameObject[] platforms; // 미리 생성된 바닥 모음
    private int currentIndex = 0; // 현재 스폰할 바닥의 순번

    private Vector2 poolPosition = new Vector2 (0, -20); // 처음 바닥들을 생성할때 화면 밖에 숨겨둘 위치
    private float lastSpawnTime; // 마지막으로 스폰한 시점

    void Start () {
        // 변수들을 초기화하고 사용할 바닥 오브젝트들을 미리 생성한다
        platforms = new GameObject[count]; // 바닥 배열의 길이를 생성할 갯수만큼으로 지정

        // count 만큼 루프하면서
        for (int i = 0; i < count; i++) {
            // 바닥을 생성하여 배열에 저장
            platforms[i] = Instantiate (platformPrefab, poolPosition, Quaternion.identity);
        }

        timeBetSpawn = 0f; // 마지막 스폰 시점을 초기화
    }

    void Update () {
        // 순서를 돌아가며 바닥을 배치한다

        // 게임 오버 상태에서는 동작하지 않는다
        if (GameManager.instance.isGameover) {
            return;
        }

        if (Time.time >= lastSpawnTime + timeBetSpawn) {
            // 만약 마지막 재배치 시점에서 timeBetSpawn 이상 시간이 흘렀다면...

            // 기록된 마지막 재배치 시점을 현재 시간으로 갱신
            lastSpawnTime = Time.time;

            // 다음번 재배치 간격을 랜덤한 시간으로 변경
            timeBetSpawn = Random.Range (timeBetSpawnMin, timeBetSpawnMax);

            // 재배치의 높이를 랜덤한 값으로 설정
            float yPos = Random.Range (yMin, yMax);

            // 사용할 현재 순번의 바닥 게임 오브젝트를 비활성화 했다가 다시 활성화하여 리셋
            // 바닥 게임 오브젝트의 Platform 컴포넌트의 OnEnable 메서드가 실행된다
            platforms[currentIndex].SetActive (false);
            platforms[currentIndex].SetActive (true);

            // 현재 순번의 바닥을 재배치하고, 순번을 넘기기
            platforms[currentIndex].transform.position = new Vector2 (xPos, yPos);
            currentIndex++;

            // 만약 마지막 순번에 도달했다면 처음 순번으로 리셋
            if (currentIndex >= count) {
                currentIndex = 0;
            }
        }
    }
}