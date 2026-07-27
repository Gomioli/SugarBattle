using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{

    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float moveSpeed;

    private KeyCode jumpKey = KeyCode.Space;
    private bool isGrounded = true;


    void Update()
    {


        playerRigidbody.velocity = new Vector3(
            Input.GetAxis("Horizontal") * moveSpeed,
            0f,
            Input.GetAxis("Vertical") * moveSpeed);



    }
    
       

    private void Jump()
    {

    }

}
