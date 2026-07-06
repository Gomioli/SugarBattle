using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using IngameDebugConsole;
using Unity.Netcode;
using Unity.Networking.Transport.Relay;
using Unity.Netcode.Transports.UTP;

public class TestRelay : MonoBehaviour
{

  /*  private async void Start()
    {
        await UnityServices.InitializeAsync(); // Initialise les services de Unity

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId); // Permet d'avoir un retour si le joueur est bien inscrit
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync(); //Donne un compte anonyme (plutôt qu'un compte qui nécessite une connexion)
    }*/



    [ConsoleMethod("CreateRelay", "Cree un relay")]
    public static async void CreateRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3); // crée une allocation pour le relay

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId); // crée une variable joinCode où on y met le code pour rejoindre le relay de la variable allocation
            Debug.Log(joinCode);


            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");  

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartHost();

        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }

    }

    [ConsoleMethod("JoinRelay", "Rejoindre un relay")]
    public static async void JoinRelay(string joinCode)
    {
        try
        {
            Debug.Log("Joining Relay with " + joinCode);
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);


            RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");  

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartClient();

        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }

}
