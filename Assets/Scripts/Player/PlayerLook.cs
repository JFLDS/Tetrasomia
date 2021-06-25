using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    // Variables
    [SerializeField] private Transform player;
    [SerializeField] public static float sensitivity = 10f;

    private float x = 0f;
    private float y = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Cursor Delock for Menus
        if (MenuPause.isOn || MenuOptions.isOn) {
            if (Cursor.lockState != CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            return; 
        }

        // Cursor Lock if no reason to Delock
        if(Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Reading the Input
        x -= Input.GetAxisRaw("Mouse Y") * sensitivity;
        y += Input.GetAxisRaw("Mouse X") * sensitivity;

        // Clamping
        x = Mathf.Clamp(x, -90f, 45f);

        // Rotation
        transform.localRotation = Quaternion.Euler(x, 0f, 0f);
        player.transform.localRotation = Quaternion.Euler(0f, y, 0f);

        // Cursor Locking
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked) Cursor.lockState = CursorLockMode.None;
        }
    }
}



    
