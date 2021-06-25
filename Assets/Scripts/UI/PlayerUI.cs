using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform healthbarFill;

    [SerializeField]
    private Text ammoText;

    private Player player;

    private WeaponManager weaponManager;

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

        weaponManager = player.GetComponent<WeaponManager>();
    }

    private void Update()
    {
        SetHealthAmount(player.GetHealthPct());
        SetAmmoAmount(weaponManager.currentMagazineSize);

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

    void SetAmmoAmount(int _amount)
    {
        ammoText.text = _amount.ToString();
    }
}
