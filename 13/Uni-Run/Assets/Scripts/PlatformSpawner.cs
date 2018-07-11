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

    }

    void Update () {
        // 순서를 돌아가며 바닥을 배치한다

    }
}