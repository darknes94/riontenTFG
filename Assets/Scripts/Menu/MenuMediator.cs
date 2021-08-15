

using UnityEngine;

public class MenuMediator : MonoBehaviour
{
    [SerializeField]  MainMenu _mainMenu;
    [SerializeField]  OptionsMenu _optMenu;

    
    void Awake()
    {
        _mainMenu.Configure(this);
        _optMenu.Configure(this);

        _optMenu.Hide();
    }


    public void ChangeToOptionsMenu()
    {
        _mainMenu.Hide();
        _optMenu.Show();
    }

    public void BackToMainMenu()
    {
        _mainMenu.Show();
        _optMenu.Hide();
    }

    public void StartGame()
    {
        _mainMenu.NewGame();
    }

    public void ExitGame()
    {
        _mainMenu.ExitGame();
    }
}
