using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    MenuMediator _mediator;
    Button btnNewGame, btnOptions, btnExit;

    private void Awake()
    {
        btnNewGame = transform.Find("btnNewGame").gameObject.GetComponent<Button>();
        btnOptions = transform.Find("btnOptions").gameObject.GetComponent<Button>();
        btnExit = transform.Find("btnExit").gameObject.GetComponent<Button>();

        btnNewGame.onClick.AddListener(NewGame);
        btnOptions.onClick.AddListener(ChangeToOptionsMenu);
        btnExit.onClick.AddListener(ExitGame);
    }

    public void Configure(MenuMediator menuMediator)
    {
        _mediator = menuMediator;
    }

    public void Hide()
    {
        transform.gameObject.SetActive(false);
    }

    public void Show()
    {
        transform.gameObject.SetActive(true);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ChangeToOptionsMenu()
    {
        _mediator.ChangeToOptionsMenu();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
