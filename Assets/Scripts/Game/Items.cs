using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    protected int _id;
    protected string _nameObj;
    protected string _description;
    protected Inventory inv;

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public string NameObj
    {
        get { return _nameObj; }
        set { _nameObj = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        inv = FindObjectOfType<Inventory>();
    }

    //void OnCollisionStay2D(Collision2D col)
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inv.AddObjectInvt(_id, _nameObj, _description);
            if (_id == 0)
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
