
using UnityEngine;
using Mirror;
public class PlayerSetup : MonoBehaviour
{
    [SerializeField]
    Behaviour[] componentToDisable;
    
    private void Start()
    {
        if( !isLocalPlayer)
        {
            //
            for(int i = 0; i < componentToDisable.Length; i++)
            {
                componentToDisable[i].enabled = false;
            }

        }
    }
}
