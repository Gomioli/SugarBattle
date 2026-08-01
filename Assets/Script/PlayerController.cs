using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{

    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float moveSpeed;
    [SerializeField] Transform orientation;

    public bool isAttacking = false;
    private bool isGrounded = true;
    public float jumpForce = 2f;


    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
            isAttacking = true;
        }

    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        Vector3 velocity = playerRigidbody.velocity;
        Vector3 horizontalVelocity = moveDirection.normalized * moveSpeed;
        playerRigidbody.velocity = new Vector3(horizontalVelocity.x, velocity.y, horizontalVelocity.z);
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

    private void Attack()
    {
        print("A attaqué");
    }

}
