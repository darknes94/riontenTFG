using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected GameObject interactIcon;
    protected bool canActivate;
    protected GameController gameCont;

    protected void Awake()
    {
        interactIcon = gameObject.transform.GetChild(0).gameObject;
        interactIcon.SetActive(false);
        canActivate = false;
        
    }

    protected void Start()
    {
        gameCont = GameController.Instance;
    }

    protected void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E)) &&
            canActivate)
        {
            Interact();
        }
    }

    public virtual void Interact()
    {
    }

    public virtual void OnTriggerStay2D(Collider2D collision)
    {
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
    }

    protected void OpenInteractableIcon()
    {
        interactIcon.SetActive(true);
    }

    protected void CloseInteractableIcon()
    {
        interactIcon.SetActive(false);
    }
}
