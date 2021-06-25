using System.Collections.Generic;
using System.Linq;
using UnityEngine;      
using Mirror;

public class GameManager : NetworkBehaviour
{
    private const string playerIdPrefix = "Player";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public static GameManager instance;

    public delegate void OnPlayerKilledCallBack(string killed, string killer);
    public OnPlayerKilledCallBack onPlayerKilledCallBack;

    private NetworkManager networkManager;

    [SerializeField]
    private GameObject pauseMenu;

    public void Awake()
    {
        networkManager = NetworkManager.singleton;
        if (instance == null)
        {
            instance = this;
            return;
        }
        Debug.LogError("more than one instance of GameManager in the scene");
    }

    // An Update to finish the game if a player has 30 kills
    public void Update()
    {
        foreach (Player player in GetAllPlayers())
        {
            if (player.kills == 30)
            {
                Cursor.lockState = CursorLockMode.None;
                if (isClientOnly)
                {
                    networkManager.StopClient();
                }
                else
                {
                    networkManager.StopHost();
                }
            }
        }
    }

    public static void RegisterPlayer(string netID, Player player)
    {
        string playerId = playerIdPrefix + netID;
        players.Add(playerId, player);
        player.transform.name = playerId;
    }

    public static void UnregisterPlayer(string playerId)
    {
        players.Remove(playerId);
    }

    public static Player GetPlayer(string playerId)
    {
        return players[playerId];
    }

    public static Player[] GetAllPlayers()
    {
        return players.Values.ToArray();
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        MenuPause.isOn = pauseMenu.activeSelf;
    }
}
