using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public Enemy enemyPrefab;
	public Transform[] spawnPositions;

	private LivingEntity playerEntity;
	void Start () {
		playerEntity = FindObjectOfType<PlayerHealth> ();

		InvokeRepeating ("Spawn", 1f, 7f);

	}

	void Spawn () {

		for (int i = 0; i < 5; i++) {

			int randomSel = Random.Range (0, spawnPositions.Length);
			Enemy createdEnemey = Instantiate (enemyPrefab, spawnPositions[randomSel].position, spawnPositions[randomSel].rotation);
			createdEnemey.Setup (1.5f, 30f, 100f, Random.ColorHSV(), playerEntity);
		}
	}

	// Update is called once per frame
	void Update () {

	}
}