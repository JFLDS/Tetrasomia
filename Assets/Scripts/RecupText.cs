using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class RecupText : MonoBehaviour
{
    NetworkManager manager;

    void Awake()
    {
        manager = GetComponent<NetworkManager>();
    }

    public void A(Text networkAddress)
    {
        manager.networkAddress = networkAddress.text;
    }
}
