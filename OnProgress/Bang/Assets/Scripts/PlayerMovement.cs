using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 3f;
    public float rotateSpeed = 90f;
    private PlayerInput playerInput;
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    void Start () {
        playerInput = GetComponent<PlayerInput> ();
        playerRigidbody = GetComponent<Rigidbody> ();
        playerAnimator = GetComponent<Animator> ();
    }

    void OnDisable () {
        playerRigidbody.isKinematic = true;
    }

    // Update is called once per frame
    void Update () {
        Rotate ();
        Move ();

        playerAnimator.SetFloat ("Move", playerInput.move);
        playerAnimator.SetFloat ("Rotate", playerInput.rotate);

    }

    private void Move () {
        playerRigidbody.position += playerInput.move * transform.forward * moveSpeed * Time.deltaTime;
    }

    void Rotate () {
        playerRigidbody.rotation *= Quaternion.Euler (0, playerInput.rotate * rotateSpeed * Time.deltaTime, 0f);
    }

    void OnTriggerEnter (Collider other) {
        Item item = other.GetComponent<Item> ();

        if (item != null) {
            item.Use (gameObject);
        }
    }
}