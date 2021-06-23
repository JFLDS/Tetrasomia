using UnityEngine;
using Mirror;

public class RecupText : MonoBehaviour
{
    NetworkManager manager;

    void Awake()
    {
        manager = GetComponent<NetworkManager>();
    }

    void OnGUI()
    {
        if (!NetworkClient.isConnected && !NetworkServer.active)
        {
            manager.networkAddress = GUI.TextField(new Rect(30, 30, 95, 20), manager.networkAddress);
        }
    }
}
