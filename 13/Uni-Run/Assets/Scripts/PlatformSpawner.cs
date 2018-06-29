using UnityEngine;
using System.Collections;

public class PlatformSpawner : MonoBehaviour {

    public GameObject platformPrefab;   // 생성할 바닥의 원본 프리팹
    public int count = 3;   // 게임이 시작될때 준비할 바닥 오브젝트의 갯수
    public float timeBetSpawnMin = 1.25f;   // 다음번 스폰까지의 시간 간격의 최솟값
    public float timeBetSpawnMax = 2.25f;   // 다음번 스폰까지의 시간 간격의 최댓값

    private float timeBetSpawn; // 다음번 스폰까지의 시간 간격

    public float yMin = -3.5f;  // 스폰할 위치의 최소 y값
    public float yMax = 1.5f;   // 스폰할 위치의 최대 y값


    private GameObject[] platforms; // 미리 생성된 바닥 모음
    private int currentIndex = 0;   // 현재 스폰할 바닥의 순번


    private Vector2 startPosition = new Vector2(0, -25);    // 처음 바닥들을 생성할때 화면 밖에 숨겨둘 위치
    private float xPos = 20f;   // 스폰할 위치의 x 값
    private float lastSpawnTime;    // 마지막으로 스폰한 시점


    void Start() {
        platforms = new GameObject[count]; // 바닥 배열의 길이를 생성할 갯수만큼으로 지정

        // count 만큼 루프하면서
        for (int i = 0; i < count; i++)
        {
            // 바닥을 생성하여 배열에 저장
            platforms[i] = Instantiate(platformPrefab, startPosition, Quaternion.identity);
        }

        timeBetSpawn = 0f; // 마지막 스폰 시점을 초기화
    }

    void Update() {
        if(GameManager.instance.isGameover) {
            return;
        }

        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            lastSpawnTime = Time.time;
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            //Set a random y position for the column
            float yPos = Random.Range(yMin, yMax);

            //...then set the current column to that position.
            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);
            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);

            //Increase the value of currentColumn. If the new size is too big, set it back to zero
            currentIndex++;

            if (currentIndex >= count)
            {
                currentIndex = 0;
            }
        }
    }
}