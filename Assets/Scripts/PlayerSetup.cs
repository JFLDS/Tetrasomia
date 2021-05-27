
using UnityEngine;
using Mirror;
public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    [SerializeField]
    private GameObject playerUIPrefab;
    private GameObject playerUIInstance;

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

        GetComponent<Player>().Setup();

        //UI local
        playerUIInstance = Instantiate(playerUIPrefab);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string netId = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();

        GameManager.RegisterPlayer(netId, player);
    }

    private void AssignRemoteLayer() { gameObject.layer = LayerMask.NameToLayer(remoteLayerName); }

    private void DisableComponents() {
        for (int i = 0; i < componentToDisable.Length; i++) componentToDisable[i].enabled = false;
    }
    private void OnDisable()
    {
        Destroy(playerUIInstance);

        if (sceneCamera != null) sceneCamera.gameObject.SetActive(true);

        GameManager.UnregisterPlayer(transform.name);
    }
}
