using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour {
    public Rigidbody myRigidbody;

    void Start() {
        myRigidbody.AddForce(0, 500, 0);
    }
}