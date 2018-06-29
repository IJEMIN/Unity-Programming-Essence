using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isGameover = false;
    public static GameManager instance;

    public Text scoreUI;
	public GameObject gameoverUI;

    private int score = 0;

    // Use this for initialization
    void Awake()
    {

        if (instance != null)
        {
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void AddScore(int newScore)
    {

    }

	void Update()
	{

	}

    public void OnPlayerDead()
    {

    }
}
