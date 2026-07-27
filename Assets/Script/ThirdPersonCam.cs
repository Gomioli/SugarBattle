using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Third : MonoBehaviour
{

    [SerializeField] Transform orientation;
    [SerializeField] Transform player;
    [SerializeField] Transform playerObj;
    [SerializeField] Rigidbody rb;

    public float rotationSpeed;

    [SerializeField] Transform combatLookAt;

    public CameraStyle currentStyle;

    public enum CameraStyle
    {
        Basic,
        Combat
    }
}
