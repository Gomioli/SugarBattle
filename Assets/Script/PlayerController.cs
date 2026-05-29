using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float moveSpeed;

    void Start()
    {
        
    }


    void Update()
    {
        playerRigidbody.velocity = new Vector3(
            Input.GetAxis("Horizontal") * moveSpeed,
            0f,
            Input.GetAxis("Vertical") * moveSpeed);
    }
}
