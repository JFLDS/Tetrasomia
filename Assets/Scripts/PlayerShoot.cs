using UnityEngine;
using Mirror;

public class PlayerShoot : NetworkBehaviour
{
    public PlayerWeap weapon;
    [SerializeField]
    private new Camera camera;
    [SerializeField]
    private LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        if (camera == null) Debug.LogError("Pas de caméra."); this.enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            Debug.Log("ca tire belek");
        }

    }

    [Client]
    private void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, weapon.range, mask))
        {
            Debug.Log("Objet touché : " + hit.collider.name);
            //if (hit.collider.tag == "Player" || hit.collider.tag == "Ground")  CmdPlayerShot(hit.collider.name, weapon.damage);
        }
    }

    [Command]
    private void CmdPlayerShot(string Pname, float damage)
    {
        Debug.Log(Pname + " a été touché.");

        Player player = GameManager.GetPlayer(Pname);
        player.RpcTakeDamage(damage);
    }
}
