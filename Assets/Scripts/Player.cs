using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SerializeField]
    private float maxHealth = 100f;

    //valeur modifiée chez toutes les instances
    [SyncVar]
    private float currentHealth;

    public int kills;
    public int deaths;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabledOnStart;


    private bool firstSetup = true;

    public void Setup()
    {
        if (isLocalPlayer)
        {
            //Changement de caméra
            
        }
        CmdBroadcastNewPlayerSetup();
    }

    [Command] //(ignoreAuthority = true)
    private void CmdBroadcastNewPlayerSetup()
    {
        RpcSetupPlayerOnAllClients();
    }

    [ClientRpc]
    private void RpcSetupPlayerOnAllClients()
    {
        if (firstSetup)
        {
            wasEnabledOnStart = new bool[disableOnDeath.Length];
            for (int i = 0; i < disableOnDeath.Length; i++)
            {
                wasEnabledOnStart[i] = disableOnDeath[i].enabled;
            }
            firstSetup = false;
        }
        SetDefaults();
    }

    public void SetDefaults()
    {
        currentHealth = maxHealth;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabledOnStart[i];
        }

        //Réactive le collider du joueur
        Collider col = GetComponent<Collider>();
        if(col != null)
        {
            col.enabled = true;
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTimer);
        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        yield return new WaitForSeconds(0.1f);
        Setup();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(transform.name + " a maintenant : " + currentHealth + " PV");
    }
}
