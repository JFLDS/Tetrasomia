using UnityEngine;
using UnityEngine.UI;
using Mirror;


public class RecupText : MonoBehaviour
{
    public static RecupText instance;

    public static string renommer;

    NetworkRoomManager manager;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
        manager = GetComponent<NetworkRoomManager>();
    }

    public void ChangeAddress(Text networkAddress)
    {
        manager.networkAddress = networkAddress.text;
    }

    public void Renommer(Text username)
    {
        renommer = username.text;
    }
}
