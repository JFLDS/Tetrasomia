using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static Dictionary<string, Player> players = new Dictionary<string, Player>();
    private const string playerIdPrefix = "Player";

    public static void RegisterPlayers(string netID, Player player)
    {
        string playerID = playerIdPrefix + netID;
        players.Add(playerID, player);
        player.transform.name = playerID;
    }

    public static void UnregisterPlayer(string playerID)
    {
        players.Remove(playerID);
    }

    public static Player GetPlayer(string playerID)
    {
        return players[playerID];
    }
}
