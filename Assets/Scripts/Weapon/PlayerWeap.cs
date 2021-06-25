using UnityEngine;

[System.Serializable]
public class PlayerWeap
{
    public string nameW = "AR-06";
    public float damage = 10f;
    public float range = 100f;

    public float fireRate = 0f;

    public int magSize = 30;

    public float reloadTime = 2f;
    public GameObject GFX;
}
