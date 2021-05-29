
using UnityEngine;
using Mirror;
using System.Collections;

public class Player : NetworkBehaviour
{
    [SyncVar]
    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    [SerializeField]
    private float maxHealth = 100f;

    [SyncVar]
    private float currentHealth;

    public int kills;
    public int deaths;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabledOnStart;

    public void Setup()
    {
        wasEnabledOnStart = new bool[disableOnDeath.Length];
        for(int i = 0; i < disableOnDeath.Length; i++)
        {
            wasEnabledOnStart[i] = disableOnDeath[i].enabled;
        }

        SetDefaults();
    }
   
    public void SetDefaults()
    {
        isDead = false;
        currentHealth = maxHealth;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabledOnStart[i];
        }

        Collider col = GetComponent<Collider>();
        if(col != null)
        {
            col.enabled = true;
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTimer);
        SetDefaults();
        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
    }

    [ClientRpc]
    public void RpcTakeDamage(float amount, string sourceID)
    {
        if (isDead) return;
        currentHealth -= amount;
        Debug.Log(transform.name + "a maintenant : " + currentHealth + "points de vies.");

        if(currentHealth <= 0)
        {
            Die(sourceID);
        }
    }

    private void Die(string sourceID)
    {
        isDead = true;

        Player sourcePlayer = GameManager.GetPlayer(sourceID);
        if(sourcePlayer != null)
        {
            sourcePlayer.kills++;
        }

        deaths++;

        //Désactive les components du joueur lors de la mort
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        //Désactive le collider du joueur
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        Debug.Log(transform.name + "à passé l'arme à gauche");

        StartCoroutine(Respawn());
    }
}
