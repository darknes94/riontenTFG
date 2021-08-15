using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    MenuMediator _mediator;

    Button btnBack;

    private void Awake()
    {

        btnBack = transform.Find("btnBack").gameObject.GetComponent<Button>();

        btnBack.onClick.AddListener(Back);
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


    public void Back()
    {
        _mediator.BackToMainMenu();
    }
}
