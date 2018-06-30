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

    }

    void OnCollisionEnter2D(Collision2D collision)
	{

	}
}
