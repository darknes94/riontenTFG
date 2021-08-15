using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Animator animator;
    Rigidbody2D myRB;
    float xInitial, yInitial;
    float speed;
    bool movRight;
    float move;
    public float jumpPower;
    bool jump, grounded, canSliding;

    bool dash = false;

    Inventory inv;
    public GameObject magicBall;

    [SerializeField] Transform wallTouch;
    [SerializeField] LayerMask masksWall;
    const float circleRadius = .2f;

    [SerializeField] LayerMask m_Interactable;
    
    Vector2 boxSize;
    RaycastHit2D hit;

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        myRB = GetComponent<Rigidbody2D>();
        inv = FindObjectOfType<Inventory>();
        

        xInitial = transform.position.x;
        yInitial = transform.position.y;

        speed = 150f;
        move = 0f;
        jumpPower = 6.5f;

        movRight = true;
        jump = false;
        grounded = true;
        canSliding = false;

        boxSize = new Vector2(0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        /*Debug.Log("U: "+Time.deltaTime + " seg " +
            1.0f / Time.deltaTime + " FPS");*/
        if (!inv.inventoryEnabled) { 
            
            if (move > 0 && !movRight)
                FlipAnimacion();
            else if (move < 0 && movRight)
                FlipAnimacion();

            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                jump = true;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                dash = true;
            }


            if (Input.GetKeyDown(KeyCode.V)) //TO DO, comprobar amuleto equipado
            {
                // esto permite lanzar muchas bolas, TO DO limitar a 3
                GameObject ball = Instantiate(magicBall, (transform.position +
                    new Vector3(transform.localScale.x * 0.5f, -0.2f)), Quaternion.identity) as GameObject;
                Vector2 direction = new Vector2(transform.localScale.x, 0);
                ball.GetComponent<MagicBall>().direction = direction;
            }

            /*if (move==0)
                animator.Play("Parar");
            else
                animator.Play("Empezar_Caminar");*/

            //myRB.velocity = new Vector2(move * speed, myRB.velocity.y);
            //animator.SetFloat("speed", Mathf.Abs(myRB.velocity.x));
            //print(myRB.velocity.magnitude);

            /*if (Input.GetKeyDown("right"))
            {
                if (!movDer)
                    FlipAnimacion();

                animator.Play("Empezar_Caminar");
                transform.position += new Vector3(transform.position.x*vel * Time.deltaTime, 0, 0);
                print(transform.position.x);
            }

            if (Input.GetKeyDown("left"))
            {
                if (movDer)
                    FlipAnimacion();

                animator.Play("Empezar_Caminar");
                transform.position = new Vector3(transform.position.x - vel * Time.deltaTime, 0, 0);
            }

            if ((Input.GetKeyUp("right")) || Input.GetKeyUp("left"))
            {
                animator.Play("Parar");
            }*/
        }
    }

    private void FixedUpdate()
    {
        move = Input.GetAxis("Horizontal");
        if (jump)
        {
            myRB.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }

        if (dash)
        {
            Vector2 direction = new Vector2(transform.localScale.x, 0);
            //myRB.AddForce(direction * jumpPower, ForceMode2D.Impulse);
            //myRB.velocity = new Vector2(move * jumpPower, myRB.velocity.y);
            dash = false;
        }

        

        // Si no esta en el suelo
        if (!grounded)
        {
            Collider2D[] collidersWall = Physics2D.OverlapCircleAll(
                wallTouch.position, circleRadius, masksWall);

            if (collidersWall.Length > 0)
            {
                canSliding = true;
                Debug.Log("collidersWall.Length: " + collidersWall.Length);
            }
            else
            {
                Debug.Log("Colisiona algo pero NO PARED");
            }

            

            /*for (int i = 0; i < collidersWall.Length; i++)
            {
                if (collidersWall[i].gameObject != null)
                {
                    /*isDashing = false;
                    m_IsWall = true;*/
                /*}
            }*/
        }
        else
        {
            canSliding = false;
        }

        /*if (canSliding)
        {
            myRB.velocity = new Vector2(myRB.velocity.x, myRB.velocity.y * speed);
        }
        else
        {*/
            myRB.velocity = new Vector2((move * speed)* Time.fixedDeltaTime, myRB.velocity.y);
        //}
        Debug.Log(Time.deltaTime+" seg "+
            1.0f / Time.deltaTime+" FPS");
    }

    void FlipAnimacion()
    {
        movRight = !movRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = true;
            //print("cosa: " + grounded);
        }

        
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = false;
            //print("cosa2: " + grounded);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Geiser") && inv.IsUmbrella())
        {
            jump = true;
            print("Geiser");
        }

        if (collision.CompareTag("Wall") && inv.IsGloves())
        {
            jump = true;
            print("Wall and gloves");
        }
    }

    public void ResetPosition()
    {
        transform.position = new Vector3(xInitial, yInitial, 0);
    }


    // Para ver la colision del personaje con la pared
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(
            gameObject.transform.GetChild(0).transform.position, 
            circleRadius);
    }

    // Para saber si el personaje tiene la llave 'nkey' en el inventario
    public bool IsKey(int nkey)
    {
        return inv.IsKey(nkey);
    }















    // No se utiliza por ahora
    public void CheckInteraction()
    {
        Debug.Log("Interactuo");
        /*RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position,boxSize,
            0, Vector2.zero);

        if (hits.Length > 0)
        {
            foreach(RaycastHit2D rc in hits)
            {
                if (rc.transform.GetComponent<Interactable>())
                {
                    rc.transform.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }*/

        // RayCast con dibujo y todo
        /*Vector2 direction = Vector2.left;
        if (movRight)
            direction = Vector2.right;

        //Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask);
        hit = Physics2D.Raycast(transform.position, direction,
            1.5f, m_Interactable);

        Debug.DrawRay(transform.position, direction.normalized * 1.5f, Color.red, 3f);

        if (hit.collider != null)
        {
            Debug.Log("Objeto INTERACCCCCCCCCCcc");
            hit.collider.gameObject.GetComponent<Interactable>().Interact();
        }*/
    }

    
}
