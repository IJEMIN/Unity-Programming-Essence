using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public Enemy enemyPrefab;
	public Transform[] spawnPositions;

	private LivingEntity playerEntity;
	void Start () {
		playerEntity = FindObjectOfType<PlayerHealth> ();

		for (int i = 0; i < 10; i++) {
			Enemy createdEnemey = Instantiate (enemyPrefab, spawnPositions[0].position, spawnPositions[0].rotation);
			createdEnemey.Setup (0.5f, 30f, 100f, Color.red, playerEntity);
		}

	}

	// Update is called once per frame
	void Update () {

	}
}