using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    protected int ID;
    protected string nameObj;
    protected string description;
    protected Inventory inv;

    // Start is called before the first frame update
    void Start()
    {
        inv = FindObjectOfType<Inventory>();
    }

    public int GetID ()
    {
        return ID;
    }
    public void SetID (int newID)
    {
        ID = newID;
    }

    public string GetName()
    {
        return nameObj;
    }
    public void SetName(string newN)
    {
        nameObj = newN;
    }

    public string GetDescription()
    {
        return description;
    }
    public void SetDescription(string newD)
    {
        description = newD;
    }

    //void OnCollisionStay2D(Collision2D col)
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inv.AddObjectInvt(ID, nameObj, description);
            if (ID == 0)
                inv.UpdatePickaxe();
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }

        /*
        if (collision.CompareTag("Player"))
        //if (collision.gameObject.tag == "Player")

        //FindObjectOfType<GameController>().SendMessage("AnotarItemRecogido");
        FindObjectOfType<Inventory>().ActiveSlot(ID);*/
    }
}
