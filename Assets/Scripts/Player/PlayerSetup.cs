using UnityEngine;
using Mirror;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    [SerializeField]
    private GameObject playerUIPrefab;
    private GameObject playerUIInstance;

    [SerializeField]
    private GameObject camGFX;

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

            playerUIInstance = Instantiate(playerUIPrefab);
            PlayerUI ui = playerUIInstance.GetComponent<PlayerUI>();
            if (ui == null)
            {
                Debug.LogError("Pas de Component Player UI sur playerUIInstance");
            }
            else
            {
                ui.SetPlayer(GetComponent<Player>());
            }
        }
        GetComponent<Player>().Setup();

        CmdSetUsername(transform.name, RecupText.renommer);
    }

    [Command]
    void CmdSetUsername(string playerID, string username)
    {
        Player player = GameManager.GetPlayer(playerID);
        if (player != null)
        {
            player.username = username;
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string netId = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();

        GameManager.RegisterPlayer(netId, player);
    }

    private void AssignRemoteLayer() {
        //gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
        Util.SetLayerRecursively(gameObject, LayerMask.NameToLayer(remoteLayerName));
        Util.SetLayerRecursively(camGFX, LayerMask.NameToLayer("Hide"));
    }

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
