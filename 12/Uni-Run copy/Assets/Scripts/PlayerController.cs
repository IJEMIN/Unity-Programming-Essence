using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    public float jumpForce =  600f;
    private bool isGrounded = false;

    private bool isDead = false;

    private Rigidbody2D playerRigidbody;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            return;
        }

        animator.SetBool("Grounded",isGrounded);

        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
        }
        else if(Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Death" && !isDead)
        {
            animator.SetTrigger("Die");
            playerRigidbody.velocity = Vector2.zero;
            isDead = true;

            GameManager.instance.OnPlayerDead();
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        isGrounded = false;
    }
}
