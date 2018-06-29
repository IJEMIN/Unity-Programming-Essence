using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public float rotationSpeed = 60;
	
	void Update () {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
	}
}
