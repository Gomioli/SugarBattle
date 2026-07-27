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


        playerRigidbody.velocity = new Vector3(
            Input.GetAxis("Horizontal") * moveSpeed,
            0f,
            Input.GetAxis("Vertical") * moveSpeed);

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
            isGrounded = false;
        }

    }
    
       

    private void Jump()
    {
        isGrounded = false;
        playerRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

}
