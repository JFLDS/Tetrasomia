using UnityEngine;
using Mirror;

public class WeaponManager : NetworkBehaviour
{
    [SerializeField]
    private PlayerWeap primaryWeapon;

    private PlayerWeap currentWeapon;

    [SerializeField]
    private Transform WeaponPivot;
    [SerializeField]
    private string weaponLayerName = "Weapon";

    void Start()
    {
        EquipWeapon(primaryWeapon);
    }

    public PlayerWeap GetCurrentWeapon() { return currentWeapon; }

    void EquipWeapon(PlayerWeap _weapon)
    {
        currentWeapon = _weapon;
        GameObject weaponIns = Instantiate(_weapon.GFX, WeaponPivot.position, WeaponPivot.rotation);
        weaponIns.transform.SetParent(WeaponPivot);

        if (isLocalPlayer)
        {
            weaponIns.layer = LayerMask.NameToLayer(weaponLayerName);
        }
    }
}
