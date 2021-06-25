using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreboard : MonoBehaviour
{
    [SerializeField]
    Text usernameText;
    [SerializeField]
    Text killsText;
    [SerializeField]
    Text deathsText;

    public void Setup(Player player)
    {
        if (player.username != "") usernameText.text = player.username;
        else usernameText.text = player.name;
        killsText.text = "Kills : " + player.kills;
        deathsText.text = "Deaths : " + player.deaths;
    }
}
