using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    //Variable
    [SerializeField] private Transform player;
    [SerializeField] private float sensitivity = 10f;

    private float x = 0f;
    private float y = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        //Initiate the Slider value to half way
        m_MySliderValue = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuPause.isOn || MenuOptions.isOn) {
            if (Cursor.lockState != CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            return; 
        }

        if(Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        //Reading the Input
        x -= Input.GetAxisRaw("Mouse Y") * sensitivity;
        y += Input.GetAxisRaw("Mouse X") * sensitivity;

        //Clamping
        x = Mathf.Clamp(x, -90f, 45f);

        //Rotation
        transform.localRotation = Quaternion.Euler(x, 0f, 0f);
        player.transform.localRotation = Quaternion.Euler(0f, y, 0f);

        //Cursor Locking
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked) Cursor.lockState = CursorLockMode.None;
        }
    }

    //Value from the slider, and it converts to volume level
    float m_MySliderValue;

    void OnGUI()
    {
        if (MenuOptions.isOn)
        {
            //Create a horizontal Slider that controls volume levels. Its highest value is 1 and lowest is 0
            m_MySliderValue = GUI.HorizontalSlider(new Rect(25, 25, 200, 60), m_MySliderValue, 0.0F, 10.0F);
            //Makes the volume of the Audio match the Slider value
            sensitivity = m_MySliderValue;
        }
    }
}



    
