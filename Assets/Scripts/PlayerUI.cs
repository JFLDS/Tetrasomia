using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPause;

    [SerializeField]
    private GameObject tableauDesScores;

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

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            tableauDesScores.SetActive(true);
        }else if (Input.GetKeyUp(KeyCode.Tab))
        {
            tableauDesScores.SetActive(false);
        }
    }

    public void TogglePauseMenu()
    {
        menuPause.SetActive(!menuPause.activeSelf);
        MenuPause.isOn = menuPause.activeSelf;
    }

}
