using UnityEngine;
using UnityEngine.UI;

public class KillfeedItem : MonoBehaviour
{
    [SerializeField]
    Text text;

    public void Setup(string killed, string killer)
    {
        text.text = killer + "killed" + killed;
    }
}
