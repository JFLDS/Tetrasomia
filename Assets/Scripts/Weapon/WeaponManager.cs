using UnityEngine;
using Mirror;
using System.Collections;

public class WeaponManager : NetworkBehaviour
{
    [SerializeField]
    private PlayerWeap primaryWeapon;

    private PlayerWeap currentWeapon;

    [SerializeField]
    private GameObject camGFX;  //L'arme fixer à la camera seulement visible depuis la pov du local player!

    //[SerializeField] private Transform WeaponPivot;
    [SerializeField] private string weaponLayerName = "Weapon";

    [HideInInspector]
    public int currentMagazineSize;

    public bool isReloading = false;

    void Start()
    {
        EquipWeapon(primaryWeapon);
    }

    public PlayerWeap GetCurrentWeapon() { return currentWeapon; }
    
    void EquipWeapon(PlayerWeap _weapon)
    {
        currentWeapon = _weapon;
        currentMagazineSize = _weapon.magSize;
        //Pour le spawn de l'arme a l'achat
        /*GameObject weaponIns = Instantiate(_weapon.GFX, WeaponPivot.position, WeaponPivot.rotation);
        weaponIns.transform.SetParent(WeaponPivot);

        currentGFX = weaponIns.GetComponent<WeaponGFX>();

        if (currentGFX == null) Debug.LogError("Pas de weapon GFX sur l'arme " + weaponIns.name);
        */
        if (isLocalPlayer)
        {
            //weaponIns.layer = LayerMask.NameToLayer(weaponLayerName);
            //Util.SetLayerRecursively(weaponIns, LayerMask.NameToLayer(weaponLayerName));

            //fonctionne pas
            //On met les layer de l'arme, et de ces enfants, attaché à la camera à "Weapon"

            camGFX.layer = LayerMask.NameToLayer(weaponLayerName);
            Util.SetLayerRecursively(camGFX, LayerMask.NameToLayer(weaponLayerName));
        }
    }

    public IEnumerator Reload()
    {
        if (isReloading) yield break;
        
        isReloading = true;

        CmdOnReload();
        yield return new WaitForSeconds(currentWeapon.reloadTime);
        currentMagazineSize = currentWeapon.magSize;

        isReloading = false;
    }

    [Command]
    void CmdOnReload()
    {
        RpcOnReload();
    }

    [ClientRpc]
    void RpcOnReload()
    {
        Animator animator = GetComponentInChildren<Animator>();//GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Reload");
        }

        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(currentWeapon.Reload);
    }
}
