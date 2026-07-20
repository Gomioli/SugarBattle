using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;

public class SetupCameraPlayer : NetworkBehaviour // donne accès aux fonctionnalités du réseau Netcode.
{
    public override void OnNetworkSpawn() // est appelée automatiquement une fois que l'objet réseau a fini de spawn. Similaire à un Start().
    {
        if (IsOwner)
        {
            CinemachineFreeLook freeLook = FindObjectOfType<CinemachineFreeLook>();
            freeLook.Follow = transform;
            freeLook.LookAt = transform;
        }
    }
}
