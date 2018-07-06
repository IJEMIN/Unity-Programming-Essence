using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour {

	void Start () {
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 60));
	}

	void Update () {
		transform.Rotate (new Vector3 (0, 180, 0) * Time.deltaTime);
	}
}