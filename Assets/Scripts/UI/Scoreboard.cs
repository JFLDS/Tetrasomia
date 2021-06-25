using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField]
    GameObject playerScoreboard;

    [SerializeField]
    Transform ListPlayer;

    private void OnEnable()
    {
        // Récupérer une array de tous les joueurs du serveur
        Player[] players = GameManager.GetAllPlayers();

        // Loop sur l'array et mise en place d'une ligne de UI pour chaque joueur + remplissage de la ligne avec les bonnes données
        foreach (Player player in players)
        {
            GameObject itemGO = Instantiate(playerScoreboard, ListPlayer);
            PlayerScoreboard item = itemGO.GetComponent<PlayerScoreboard>();
            if(item != null)
            {
                item.Setup(player);
            }
        }
    }

    private void OnDisable()
    {
        // Empty the list of players
        foreach(Transform child in ListPlayer)
        {
            Destroy(child.gameObject);
        }
    }
}
