using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPause;

    private void Start()
    {
        MenuPause.isOn = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        menuPause.SetActive(!menuPause.activeSelf);
        MenuPause.isOn = menuPause.activeSelf;
    }

}
