using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed = 5f;
    private Rigidbody bulletRigidbody;

    void Start () {
        bulletRigidbody = GetComponent<Rigidbody> ();
        bulletRigidbody.velocity = transform.forward * speed;

        Destroy (gameObject, 3f);
    }

    void OnTriggerEnter (Collider other) {
        if (other.tag == "Player") {
            PlayerController playerController = other.GetComponent<PlayerController> ();

            if (playerController != null) {
                playerController.Die ();
            }
        }
    }
}