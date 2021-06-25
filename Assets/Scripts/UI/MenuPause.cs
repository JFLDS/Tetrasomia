using Mirror;

public class MenuPause : NetworkBehaviour
{
    public static bool isOn = false;

    private NetworkManager networkManager;

    public void Start()
    {
        networkManager = NetworkManager.singleton;
    }

    public void BouttonQuitter()
    {
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
