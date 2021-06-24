using UnityEngine;

public class TableauDesScores : MonoBehaviour
{
    [SerializeField]
    GameObject tableau1Joueur;

    [SerializeField]
    Transform ListeJoueur;

    private void OnEnable()
    {
        //Récupérer une array de tous les joueurs du serveur
        Player[] players = GameManager.GetAllPlayers();

        // Loop sur l'array et mise en place d'une ligne de UI pour chaque joueur + remplissage de la ligne avec les bonnes données
        foreach (Player player in players)
        {
            GameObject itemGO = Instantiate(tableau1Joueur, ListeJoueur);
            Tableau1Joueur item = itemGO.GetComponent<Tableau1Joueur>();
            if(item != null)
            {
                item.Setup(player);
            }
        }
    }

    private void OnDisable()
    {
        // Vider la liste des joueurs
        foreach(Transform child in ListeJoueur)
        {
            Destroy(child.gameObject);
        }
    }
}
