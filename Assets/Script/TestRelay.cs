using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using IngameDebugConsole;

public class TestRelay : MonoBehaviour
{

    private async void Start()
    {
        await UnityServices.InitializeAsync(); // Initialise les services de Unity

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId); // Permet d'avoir un retour si le joueur est bien inscrit
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync(); //Donne un compte anonyme (plutôt qu'un compte qui nécessite une connexion)
    }



    [ConsoleMethod("CreateRelay", "Cree un relay")]
    public async void CreateRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3); // crée une allocation pour le relay

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId); // crée une variable joinCode où on y met le code pour rejoindre le relay de la variable allocation

        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }

    }

}
