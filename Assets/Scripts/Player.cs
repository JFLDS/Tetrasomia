using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SerializeField]
    private float maxHealth = 100f;

    //valeur modifiée chez toutes les instances
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

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(transform.name + " a maintenant : " + currentHealth + " PV");
    }
}
