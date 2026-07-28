using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;

public class SetupCameraPlayer : NetworkBehaviour
{
    [SerializeField] Transform orientation;
    [SerializeField] Transform playerObj;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            CinemachineFreeLook freeLook = FindObjectOfType<CinemachineFreeLook>();
            freeLook.Follow = transform;
            freeLook.LookAt = transform;

            ThirdPersonCam thirdPersonCam = FindObjectOfType<ThirdPersonCam>();
            thirdPersonCam.player = transform;
            thirdPersonCam.orientation = orientation;
            thirdPersonCam.playerObj = playerObj;
        }
    }
}





