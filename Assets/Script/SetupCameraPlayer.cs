using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;

public class SetupCameraPlayer : NetworkBehaviour // donne accès aux fonctionnalités du réseau Netcode.
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



    public override void OnNetworkSpawn() // est appelée automatiquement une fois que l'objet réseau a fini de spawn. Similaire à un Start().
    {
        if (IsOwner)
        {
            CinemachineFreeLook freeLook = FindObjectOfType<CinemachineFreeLook>();
            freeLook.Follow = transform;
            freeLook.LookAt = transform;
        }
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
