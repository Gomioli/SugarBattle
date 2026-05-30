using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float moveSpeed;




    void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            TestClientRpc(new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } }); // Ca permet que seul le client d'ID 1 recoive un message, et pas le Host
        }

        playerRigidbody.velocity = new Vector3(
            Input.GetAxis("Horizontal") * moveSpeed,
            0f,
            Input.GetAxis("Vertical") * moveSpeed);
    }


    [ServerRpc]
    private void TestServerRpc()
    {
        Debug.Log("TestServerRpc" + OwnerClientId);
    }


    [ClientRpc]
    private void TestClientRpc(ClientRpcParams clientRpcParams)
    {
        Debug.Log("TestClientRpc");
    }

}
