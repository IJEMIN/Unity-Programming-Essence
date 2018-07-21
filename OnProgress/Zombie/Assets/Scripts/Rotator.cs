using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

public float rotationSpeed = 60f;

		// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.Rotate(0f,rotationSpeed * Time.deltaTime, 0f);
	}

}
