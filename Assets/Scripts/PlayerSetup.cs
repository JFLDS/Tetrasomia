using UnityEngine;
using Mirror;

public class playerSetup : NetworkBehaviour
{
    //tableau pour désactiver les scripts des autres joueurs
    [SerializeField]
    Behaviour[] componentsToDisable;

    Camera sceneCamera;

    [SerializeField]
    private string remoteLayerName = "Remote Player";

    private void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            sceneCamera = Camera.main;
            if(sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }

        

    }

    //place les joueurs dans un tableau
    public override void OnStartClient()
    {
        base.OnStartClient();

        string netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();
        GameManager.RegisterPlayers(netID, player);
    }

    private void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    //Necessaire pour eviter de se toucher sois meme
    private void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    //si un joueur se deco
    private void OnDisable()
    {
        //retour au menu
        if(sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }

        //supprime le joueur du dictionnaire
        GameManager.UnregisterPlayer(transform.name);
    }
}
