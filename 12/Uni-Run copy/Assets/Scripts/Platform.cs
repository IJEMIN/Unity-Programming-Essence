using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public GameObject[] obstacles;

    private bool stepped = false;

    // Use this for initialization
    public void Reset()
    {
        stepped = false;

        for (int i = 0; i < obstacles.Length; i++)
        {
            bool active = false;
            int randomNumber = Random.Range(0, 3);

            if (randomNumber == 1)
            {
                active = true;
            }

            obstacles[i].SetActive(active);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.tag == "Player" && !stepped)
		{
			stepped = true;
			GameManager.instance.AddScore(1);
		}
	}
}
