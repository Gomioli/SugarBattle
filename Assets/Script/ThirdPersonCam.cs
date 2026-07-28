using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{

    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    [SerializeField] Rigidbody rb;

    public float rotationSpeed;

    [SerializeField] Transform combatLookAt;

    public CameraStyle currentStyle;

    public enum CameraStyle
    {
        Basic,
        Combat
    }

    private void Update()
    {
        // tourne orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;


        // tourne le GO playerObj
        Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
        orientation.forward = dirToCombatLookAt.normalized;


        playerObj.forward = dirToCombatLookAt.normalized;
    }
}
