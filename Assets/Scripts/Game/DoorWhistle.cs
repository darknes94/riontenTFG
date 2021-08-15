using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWhistle : Interactable
{
    [SerializeField]
    int codeDoor;
    bool open;

    DoorWhistle()
    {
        codeDoor = 0;
        open = false;
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !open)
        {
            // si tenemos la llave silvato
            if (collision.gameObject.GetComponent<PlayerController>().
                IsKey(codeDoor))
            {
                canActivate = true;
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !open)
        {
            // si tenemos la llave silvato
            if (collision.gameObject.GetComponent<PlayerController>().
                IsKey(codeDoor))
            {
                OpenInteractableIcon();
            } 
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !open)
        {
            // si tenemos la llave silvato
            if (collision.gameObject.GetComponent<PlayerController>().
                IsKey(codeDoor))
            {
                CloseInteractableIcon();
                canActivate = false;
            } 
        }
    }

    public override void Interact()
    {
        Debug.Log("Abro puerta con el silbato.");
        open = true;
        CloseInteractableIcon();
        canActivate = false;

        //TO DO: anim abrir puerta
    }
}
