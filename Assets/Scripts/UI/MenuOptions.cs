using UnityEngine;
using Mirror;

public class MenuOptions : NetworkBehaviour
{
    public static bool isOn = false;

    private void Start()
    {
        // Initiate the Slider value to half way
        m_MySliderValue = 5f;
    }

    // Value from the slider, and it converts to sensivity of the mouse
    float m_MySliderValue;

    // UI for the slider
    void OnGUI()
    {
        if (MenuOptions.isOn)
        {
            //Create a horizontal Slider that controls sensivity. Its highest value is 10 and lowest is 0
            m_MySliderValue = GUI.HorizontalSlider(new Rect(25, 25, 200, 60), m_MySliderValue, 0.0F, 10.0F);
            //Makes the sensivity match the Slider value
            PlayerLook.sensitivity = m_MySliderValue;
        }
    }
}
