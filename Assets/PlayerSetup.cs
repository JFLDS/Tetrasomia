
using UnityEngine;
using Mirror;
public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentToDisable;

    Camera sceneCamera;
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
        else
        {
            sceneCamera = Camera.main;
            if(sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        if(sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }
}
