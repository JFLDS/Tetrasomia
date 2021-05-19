using UnityEngine;
using Mirror;

public class PlayerShoot : NetworkBehaviour
{
    public PlayerWeapon weapon;
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        if (camera == null) Debug.LogError("Pas de caméra."); this.enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) Shoot();
    }

    private void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, weapon.range, mask))
        {
            Debug.Log("Objet touché : " + hit.collider.name);
        }
    }
}
