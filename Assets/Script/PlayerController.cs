using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{

    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float moveSpeed;

    private bool isGrounded = true;
    public float jumpForce = 2f;

    void Update()
    {


        Vector3 velocity = playerRigidbody.velocity;
        velocity.x = Input.GetAxis("Horizontal") * moveSpeed;
        velocity.z = Input.GetAxis("Vertical") * moveSpeed;
        playerRigidbody.velocity = velocity;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

    }
    
       

    private void Jump()
    {
        playerRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            isGrounded = false;
        }
    }

}
