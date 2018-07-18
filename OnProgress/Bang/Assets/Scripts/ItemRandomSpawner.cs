using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemRandomSpawner : MonoBehaviour {

	public Transform sourceTransform;
	public float maxDistance = 5f;

	public GameObject[] items;

	public float timeBetSpawnMin = 1.5f;
	public float imteBetSpawnMax = 5f;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("SpawnItem", 1f, 3f);
	}

	// Update is called once per frame

	void SpawnItem () {

		Vector3 randomSourcePosition = Random.insideUnitSphere * maxDistance;
		randomSourcePosition += sourceTransform.position;

		NavMeshHit hit;
		NavMesh.SamplePosition (randomSourcePosition, out hit, maxDistance, NavMesh.AllAreas);

		Vector3 spawnPosition = hit.position + Vector3.up * 0.5f;

		Instantiate (items[Random.Range (0, items.Length)], spawnPosition, Quaternion.identity);
	}
}