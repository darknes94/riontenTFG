using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Items
{

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inv.AddKeyInvt(_id, _nameObj, _description);
            Destroy(gameObject);
        }
    }
}
