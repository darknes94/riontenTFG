using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable
{
    [SerializeField]
    int codeDoor;
    bool openDoor;

    Lever()
    {
        codeDoor = 0;
        openDoor = false;
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canActivate = true;
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OpenInteractableIcon();
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CloseInteractableIcon();
            canActivate = false;
        }
    }

    public override void Interact()
    {
        Debug.Log("Activo PALANCA");

        

        if (!openDoor)
        {
            gameCont.OpenDoorLever(codeDoor);
        }
        else
        {
            gameCont.CloseDoorLever(codeDoor);
        }

        openDoor = !openDoor;
    }
}
