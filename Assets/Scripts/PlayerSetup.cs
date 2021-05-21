
using UnityEngine;
using Mirror;
public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";
    Camera sceneCamera;
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
            if (sceneCamera != null) sceneCamera.gameObject.SetActive(false);
        }

        nomjoueur();
    }

    private void nomjoueur() {
        string Pname = "Player" + GetComponent<NetworkIdentity>().netId;
        transform.name = Pname;
    }

    private void AssignRemoteLayer() { gameObject.layer = LayerMask.NameToLayer(remoteLayerName); }

    private void DisableComponents() {
        for (int i = 0; i < componentToDisable.Length; i++) componentToDisable[i].enabled = false;
    }
    private void OnDisable()
    {
        if (sceneCamera != null) sceneCamera.gameObject.SetActive(true);
    }
}
