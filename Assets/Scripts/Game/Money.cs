using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    GameController gameCont;

    void Awake()
    {
        gameCont = GameController.Instance;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameCont.AddMoney();
            Destroy(gameObject);
        }
    }
}
