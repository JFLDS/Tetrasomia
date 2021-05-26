using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
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

    private void Awake()
    {
        SetDefaults();
    }

    public void SetDefaults()
    {
        currentHealth = maxHealth;
    }

    [ClientRpc]
    public void RpcTakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(transform.name + "a maintenant : " + currentHealth + "points de vies.");
    }
}
