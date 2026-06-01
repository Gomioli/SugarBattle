using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using IngameDebugConsole;

public class TestLobby : MonoBehaviour
{
    private static bool isInitialized = false;

    public static async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed In " + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync(); //permet d'ajouter un compte anonyme pour l'utilisateur
        isInitialized = true;
    }


    [ConsoleMethod("CreateLobby", "Cree un lobby")]
    public static async void CreateLobby()
    {
        if (!isInitialized)
        {
            Debug.LogWarning("Unity Services pas encore initialisé, attends quelques secondes !");
            return;
        }

        try
        {
            string lobbyName = "Caca";
            int maxPlayers = 4;
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers);

            Debug.Log("Created Lobby !" + lobby.Name + " " + lobby.MaxPlayers);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

}
