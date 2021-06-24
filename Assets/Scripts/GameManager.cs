using System.Collections.Generic;
using UnityEngine;      
using System.Linq;
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
    private GameObject menuPause;

    public void Awake()
    {
        networkManager = NetworkManager.singleton;
        if (instance == null)
        {
            instance = this;
            return;
        }
        Debug.LogError("plus d'une instance de GameManager dans la scène");
    }

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
        menuPause.SetActive(!menuPause.activeSelf);
        MenuPause.isOn = menuPause.activeSelf;
    }
}
