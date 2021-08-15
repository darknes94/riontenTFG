using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    public Vector2 direction;
    public bool hasHit = false;
    public float speed = 10f;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!hasHit)
            rb.velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //TO DO, aplicar danyo
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
