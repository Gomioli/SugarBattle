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

    private void Update()
    {
        // tourne le GO orientation
        Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
        orientation.forward = dirToCombatLookAt.normalized;

        // tourne le GO playerObj
        playerObj.forward = dirToCombatLookAt.normalized; 
    }
}
