   using UnityEngine;
using Mirror;

public class WeaponManager : NetworkBehaviour
{
    [SerializeField]
    private PlayerWeap primaryWeapon;

    private PlayerWeap currentWeapon;

    private WeaponGFX currentGFX;

    [SerializeField]
    private GameObject camGFX;  //L'arme fixer à la camera seulement visible depuis la pov du local player!

    [SerializeField] private Transform WeaponPivot;
    [SerializeField] private string weaponLayerName = "Weapon";

    void Start()
    {
        EquipWeapon(primaryWeapon);
    }

    public PlayerWeap GetCurrentWeapon() { return currentWeapon; }

    public WeaponGFX GetCurrentGFX() { return currentGFX; }

    void EquipWeapon(PlayerWeap _weapon)
    {
        currentWeapon = _weapon;
        GameObject weaponIns = Instantiate(_weapon.GFX, WeaponPivot.position, WeaponPivot.rotation);
        weaponIns.transform.SetParent(WeaponPivot);

        currentGFX = weaponIns.GetComponent<WeaponGFX>();

        if (currentGFX == null) Debug.LogError("Pas de weapon GFX sur l'arme " + weaponIns.name);


        if (isLocalPlayer)
        {
            //weaponIns.layer = LayerMask.NameToLayer(weaponLayerName);
            //Util.SetLayerRecursively(weaponIns, LayerMask.NameToLayer(weaponLayerName));

            //fonctionne pas
            //On met les layer de l'arme, et de ces enfants, attaché à la camera à "Weapon"
            camGFX.layer = LayerMask.NameToLayer(weaponLayerName);
            SetLayerRecursively(camGFX, LayerMask.NameToLayer(weaponLayerName));
        }
    }

    public static void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
