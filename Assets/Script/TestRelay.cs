using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using UnityEngine;

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


    private async void CreateRelay()
    {
        try
        {
            await RelayService.Instance.CreateAllocationAsync(3);
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }

    }

}
