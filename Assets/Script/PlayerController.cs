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
            Input.GetAxis("Vertical") * moveSpeed,
            0f);
    }
}
