using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killfeed : MonoBehaviour
{

    [SerializeField]
    GameObject killfeedItemPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //GameManager.instance.onPlayerKilledCallBack += OnKill;
    }

    public void OnKill(string killed, string killer)
    {
        GameObject go = Instantiate(killfeedItemPrefab, transform);
        go.GetComponent<KillfeedItem>().Setup(killed, killer);
        Destroy(go, 5f);
    }
}
