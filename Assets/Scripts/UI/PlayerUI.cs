using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform healthbarFill;

    private Player player;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject optionMenu;

    [SerializeField]
    private GameObject scoreboard;

    public void SetPlayer(Player _player)
    {
        player = _player;
    }

    private void Start()
    {
        MenuPause.isOn = false;
    }

    private void Update()
    {
        SetHealthAmount(player.GetHealthPct());
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        if (Input.GetKeyDown(KeyCode.Tab) && !MenuPause.isOn && !MenuOptions.isOn)
        {
            scoreboard.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            scoreboard.SetActive(false);
        }
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        MenuPause.isOn = pauseMenu.activeSelf;
    }

    public void OptionMenu()
    {
        optionMenu.SetActive(!optionMenu.activeSelf);
        MenuOptions.isOn = optionMenu.activeSelf;
    }

    void SetHealthAmount(float _amount)
    {
        healthbarFill.localScale = new Vector3(1f, _amount, 1f);
    }
}
