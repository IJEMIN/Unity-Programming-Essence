using UnityEngine;
using System.Collections;

public class PlatformSpawner : MonoBehaviour
{
    public Platform platformPrefab;                                 //The column game object.
    public int count = 5;                                  //How many columns to keep on standby.
    public float timeBetSpawnMin = .3f;
    public float timeBetSpawnMax = 1.5f;

    private float timeBetSpawn = 0f;

    //How quickly columns spawn.
    public float yMin = -1.5f;                                   //Minimum y value of the column position.
    public float yMax = 0.5f;
    //Maximum y value of the column position.

    private Platform[] platforms;                                   //Collection of pooled columns.
    private int currentIndex = 0;                                  //Index of the current column in the collection.


    private Vector2 holdPosition = new Vector2(0, -25);     //A holding position for our unused columns offscreen.
    private float xPos = 15f;

    private float lastSpawnTime;


    void Start()
    {
        timeBetSpawn = 0f;
        //Initialize the columns collection.
        platforms = new Platform[count];
        //Loop through the collection... 
        for (int i = 0; i < count; i++)
        {
            //...and create the individual columns.
            platforms[i] = Instantiate(platformPrefab, holdPosition, Quaternion.identity);
        }
    }


    //This spawns columns as long as the game is not over.
    void Update()
    {
        if(GameManager.instance.isGameover)
        {
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
            platforms[currentIndex].Reset();

            //Increase the value of currentColumn. If the new size is too big, set it back to zero
            currentIndex++;

            if (currentIndex >= count)
            {
                currentIndex = 0;
            }
        }
    }
}