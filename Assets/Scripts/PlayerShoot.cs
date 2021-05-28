using UnityEngine;
using Mirror;

public class PlayerShoot : NetworkBehaviour
{
    public PlayerWeapon weapon;

    [SerializeField]
    Camera cam;

    [SerializeField]
    LayerMask mask;

    void Start()
    {
        if(cam == null)
        {
            Debug.LogError("Pas de camera renseignée sur le systeme de tir");
            //desactive le script sur le shoot
            this.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    //systeme de tir
    [Client]
    private void Shoot()
    {
        RaycastHit hit;

        if( Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask))
        {
            if(hit.collider.tag == "Player")
            {
                CmdPlayerShot(hit.collider.name, weapon.damage);
            }
        }
    }

    [Command]
    private void CmdPlayerShot(string playerId, float damage)
    {
        Debug.Log(playerId + " a été touché");

        Player player = GameManager.GetPlayer(playerId);
        player.TakeDamage(damage);
    }
}
