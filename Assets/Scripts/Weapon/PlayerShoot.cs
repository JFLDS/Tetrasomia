using UnityEngine;
using Mirror;

[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour
{   
    [SerializeField]
    private new Camera camera;

    [SerializeField]
    private LayerMask mask;

    public PlayerWeap currentWeapon;
    private WeaponManager weaponManager;

    public ParticleSystem muzzleFlash;
    public GameObject HitEffectPrefab;

    [SerializeField] 
    private GameObject camGFX;  //L'arme fixer à la camera seulement visible depuis la pov du local player!

    // Start is called before the first frame update
    void Start()
    {
        if (camera == null) { 
            Debug.LogError("Pas de caméra."); 
            this.enabled = false; 
        }

        weaponManager = GetComponent<WeaponManager>();

        //On met les layer de l'arme, et de ces enfants, attaché à la camera à "Weapon"
        Util.SetLayerRecursively(camGFX, LayerMask.NameToLayer("Weapon"));
    }

    private void Update()
    {
        currentWeapon = weaponManager.GetCurrentWeapon();
        if (MenuPause.isOn) return;

        if (currentWeapon.fireRate <= 0f)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("Shoot", 0f, 1f / currentWeapon.fireRate);
            }else if(Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shoot");
            }
        }
    }

    [Command]
    void CmdOnHit(Vector3 pos, Vector3 normal)
    {
        RpcDoHitEffect(pos,normal);
    }

    [ClientRpc]
    void RpcDoHitEffect(Vector3 pos, Vector3 normal)
    {
        GameObject hitEffect = Instantiate(HitEffectPrefab, pos, Quaternion.LookRotation(normal));
        Destroy(hitEffect, 2f);
    }

    [Command]
    void CmdOnShoot()
    {
        RpcDoShootEffect();
    }

    //Fais apparaître les effets de tir chez tlm.
    [ClientRpc]
    void RpcDoShootEffect()
    {
        muzzleFlash.Play();
    }

    [Client]
    private void Shoot()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        // Erreur servernetwork not ready
        CmdOnShoot();
        RaycastHit hit;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, currentWeapon.range, mask))
        {
            if (hit.collider.tag == "Player")  CmdPlayerShot(hit.collider.name, currentWeapon.damage, transform.name);
            //same que l'autre erreur servernetwork
             CmdOnHit(hit.point, hit.normal);
        }
    }

    [Command]
    private void CmdPlayerShot(string Pname, float damage, string sourceID)
    {
        Debug.Log(Pname + " a été touché.");

        Player player = GameManager.GetPlayer(Pname);
        player.RpcTakeDamage(damage, sourceID);
    }
}
