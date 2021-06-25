using UnityEngine;

public class Killfeed : MonoBehaviour
{
    [SerializeField]
    GameObject killfeedItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.onPlayerKilledCallBack += OnKill;
    }

    // It's make appear the UI for killfeed when a kill is done
    public void OnKill(string killed, string killer)
    {
        GameObject go = Instantiate(killfeedItemPrefab, transform);
        go.GetComponent<KillfeedItem>().Setup(killed, killer);
        Destroy(go, 3f);
    }
}
