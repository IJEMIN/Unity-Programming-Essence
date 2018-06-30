using UnityEngine;
using System.Collections;

public class PlatformSpawner : MonoBehaviour
{
    public Platform platformPrefab; // 생성할 플랫폼들의 원본이 될 플랫폼 프리팹
    public int count = 5; // 생성할 플랫폼의 갯수
    public float timeBetSpawnMin = 1.25f;
    public float timeBetSpawnMax = 2.25f;

    private float timeBetSpawn = 0f;

    //How quickly columns spawn.
    public float yMin = -3.5f;                                   //Minimum y value of the column position.
    public float yMax = 1.5f;
    //Maximum y value of the column position.

    private Platform[] platforms;                                   //Collection of pooled columns.
    private int currentIndex = 0;                                  //Index of the current column in the collection.


    private Vector2 holdPosition = new Vector2(0, -25);     //A holding position for our unused columns offscreen.
    private float xPos = 15f;

    private float lastSpawnTime;


    void Start()
    {

    }


    //This spawns columns as long as the game is not over.
    void Update()
    {
       
    }
}