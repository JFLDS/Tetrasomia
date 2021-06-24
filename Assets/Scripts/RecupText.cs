using UnityEngine;
using UnityEngine.UI;
using Mirror;


public class RecupText : MonoBehaviour
{
    NetworkRoomManager manager;

    void Awake()
    {
        manager = GetComponent<NetworkRoomManager>();
    }

    public void ChangeAddress(Text networkAddress)
    {
        manager.networkAddress = networkAddress.text;
    }

}
