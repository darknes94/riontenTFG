using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : MonoBehaviour
{
    Rigidbody2D myRB;
    BoxCollider2D box;
    PolygonCollider2D poly;
    bool activate, move;

    Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        poly = GetComponent<PolygonCollider2D>();
        activate = false;
        move = false;
        velocity = new Vector2(0, 5f);
    }

    private void FixedUpdate()
    {
        if (activate && move)
            myRB.MovePosition(myRB.position - velocity * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !activate)
        {
            activate = true;
            move = true;
            box.enabled = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Muerto");

            //TO DO: player -1 vida
            // dash hacia atras
            EraseObject();
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("suelo");
            move = false;
            poly.isTrigger = true;
            Destroy(myRB);
        }
    }

    // Destrozo del objecto al ser atacado o tocar al player
    void EraseObject()
    {
        //TO DO: animacion
    }
}
