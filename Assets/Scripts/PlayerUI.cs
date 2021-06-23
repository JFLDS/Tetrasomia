using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform healthbarFill;

    private Player player;

    [SerializeField]
    private GameObject menuPause;

    [SerializeField]
    private GameObject tableauDesScores;

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

    void SetHealthAmount(float _amount)
    {
        healthbarFill.localScale = new Vector3(1f, _amount, 1f);
    }

}
