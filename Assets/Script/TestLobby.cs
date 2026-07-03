using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using IngameDebugConsole;
using System.Threading;

public class TestLobby : MonoBehaviour
{
    private static bool isInitialized = false;

    private static Lobby hostLobby;
    private static Lobby joinedLobby;

    private float heartbeatTimer;
    private float lobbyUpdateTimer;

    private static string playerName;

    public async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed In " + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync(); //permet d'ajouter un compte anonyme pour l'utilisateur
        isInitialized = true;

        playerName = "Bebou " + UnityEngine.Random.Range(1, 99);
        Debug.Log(playerName);
    }

    private void Update()
    {
        HandleLobbyHeartbeat();
        HandleLobbyPollForUpdate();
    }

    private async void HandleLobbyHeartbeat() // permet de garder en vie le lobby apres 30 secondes. Car au bout de 30 secondes, en temps normal, le lobby disparait.
    {
        if (hostLobby != null)
        {
            heartbeatTimer -= Time.deltaTime;
            if (heartbeatTimer < 0)
            {
                float heartbeatTimerMax = 15;
                heartbeatTimer = heartbeatTimerMax;

                await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
            }
        }

    }


    private async void HandleLobbyPollForUpdate()
    {
        if (joinedLobby != null)
        {
            lobbyUpdateTimer -= Time.deltaTime;
            if (lobbyUpdateTimer < 0)
            {
                float lobbyUpdateTimerMax = 1.1f; //Les services d'Unity permettent de changer un lobby une fois toutes les secondes
                lobbyUpdateTimer = lobbyUpdateTimerMax;

                Lobby lobby = await LobbyService.Instance.GetLobbyAsync(joinedLobby.Id);
                joinedLobby = lobby; //la variable lobby (de type Lobby) et cette ligne, permette de changer les valeurs. Car le lobby ne le fait pas automatiquement
            }
        }
    }




    [ConsoleMethod("CreateLobby", "Cree un lobby")]
    public static async void CreateLobby() //il faut le mettre en public static plutot qu'en private, pour que la console puisse détecter la fonction
    {
        if (!isInitialized)
        {
            Debug.LogWarning("Unity Services pas encore initialisé, attends quelques secondes !");
            return;
        }

        try
        {
            string lobbyName = "BebouLand";
            int maxPlayers = 4;
            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions
            {
                IsPrivate = false,
                Player = GetPlayer(),
                Data = new Dictionary<string, DataObject>
                {
                    { "GameMode", new DataObject(DataObject.VisibilityOptions.Public, "CaptureTheFlag") },
                    { "Map", new DataObject(DataObject.VisibilityOptions.Public, "StrawberryIce") }
                }
            };

            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, createLobbyOptions);

            hostLobby = lobby;
            joinedLobby = lobby;

            
            Debug.Log("Created Lobby !" + lobby.Name + " " + lobby.MaxPlayers + " " + lobby.Id + " " + lobby.LobbyCode);

            PrintPlayers(hostLobby); //Lorsqu'un Lobby est crée, il y aura alors les identifiants des players affichés dans la console (donc que celui qui le crée pour le moment)
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    [ConsoleMethod("ListLobbies", "Liste les lobbies")]
    public static async void ListLobbies()
    {
        try
        {
            QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions
            {
                Count = 25, //25 lobbies vont être recherches au maxiumum
                Filters = new List<QueryFilter> //permet de faire les recherches avec des filtres
                {
                    new QueryFilter(QueryFilter.FieldOptions.AvailableSlots, "0", QueryFilter.OpOptions.GT) //ici ca va chercher les lobbies avec plus de 0 places dispo
                },
                Order = new List<QueryOrder> //trie les lobbies dans un certain ordre
                {
                    new QueryOrder(false, QueryOrder.FieldOptions.Created) //ici dans l'ordre de creation, le false precise que c'est trie dans l'ordre decroissant
                }
            };

            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync(queryLobbiesOptions);

            Debug.Log("Lobbies found : " + queryResponse.Results.Count);
            foreach (Lobby lobby in queryResponse.Results)
            {
                Debug.Log(lobby.Name + " " + lobby.MaxPlayers + " " + lobby.Data["GameMode"].Value);
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }


    [ConsoleMethod("JoinLobby", "Joindre un Lobby")]
    public static async void JoinLobbyByCode(string lobbyCode)
    {
        try
        {
            JoinLobbyByCodeOptions joinLobbyByCodeOptions = new JoinLobbyByCodeOptions
            {
                Player = GetPlayer()
            };

            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();

            Lobby lobby = await Lobbies.Instance.JoinLobbyByCodeAsync(lobbyCode, joinLobbyByCodeOptions); // rejoint le lobby avec le code correspondant
            joinedLobby = lobby;

            Debug.Log("Joined Lobby with code " + lobbyCode);
            PrintPlayers(joinedLobby);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    [ConsoleMethod("QuickJoinLobby", "Joindre un Lobby rapidement")]
    public static async void QuickJoinLobby() // permet de rejoindre un lobby random en appuyant sur un bouton
    {
        try
        {
            await LobbyService.Instance.QuickJoinLobbyAsync();
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }


    private static Player GetPlayer()
    {
        return new Player
        {
            Data = new Dictionary<string, PlayerDataObject>
                    {
                        { "PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, playerName) }
                    }
        };
    }



    [ConsoleMethod("PrintPlayersInConsole", "Afficher les joueurs dans la console")]
    public static void PrintPlayersInConsole()
    {
        PrintPlayers(joinedLobby);
    }


    private static void PrintPlayers(Lobby lobby)
    {
        Debug.Log("Players in lobby " + lobby.Name + " " + lobby.Data["GameMode"].Value + " " + lobby.Data["Map"].Value);
        foreach (Player player in lobby.Players)
        {
            Debug.Log(player.Id + " " + player.Data["PlayerName"].Value);
        }
    }


    [ConsoleMethod("UpdateLobbyGameMode", "Changer le mode de jeu du lobby")]
    public static async void UpdateLobbyGameMode(string gameMode) // Ne contient pas toutes les data du lobby (map n'y figure pas si on ne veut pas que cette data soit changée)
    {
        try
        {
            hostLobby = await Lobbies.Instance.UpdateLobbyAsync(hostLobby.Id, new UpdateLobbyOptions
            {
                Data = new Dictionary<string, DataObject>
                {
                    { "GameMode", new DataObject(DataObject.VisibilityOptions.Public, gameMode) }
                }
            });
            joinedLobby = hostLobby;

            PrintPlayers(hostLobby);
        }

        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }



    [ConsoleMethod("UpdatePlayerName", "Changer le nom du joueur")]
    public static async void UpdatePlayerName(string newPlayerName) //la fonction permet de montrer comment changer aussi les data d'un joueur
    {
        try
        {
            playerName = newPlayerName;
            await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId, new UpdatePlayerOptions
            {
                Data = new Dictionary<string, PlayerDataObject>
                {
                    { "PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, playerName) }
                }
            });
        }

        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

}
