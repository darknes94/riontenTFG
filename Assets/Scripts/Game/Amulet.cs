using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Amulet : Items
{

    public void SetColor(Vector3 color)
    {
        SpriteRenderer imgAml = GetComponent<SpriteRenderer>();
        imgAml.color = new Color(color.x / 255, color.y / 255, color.z / 255);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inv.ActiveSlotAmulet(ID, nameObj, description);
            Destroy(gameObject);
        }
    }
}
