using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public Animator animator;
    public Vector2 grav;
    public float timer;
    private float speed = 5f;
    public bool grounded;
    PolygonCollider2D coll;
    [SerializeField] private LayerMask playformLayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        animator.SetFloat("speed", (rb.velocity.x + rb.velocity.y));
        animator.SetBool("grounded", IsGrounded() );
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= 0.01f;
        Physics2D.gravity = grav;
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed left click.");

            timer = 0.2f;
        }
        if(Input.GetMouseButtonDown(0))

        if(Input.GetMouseButtonDown(1))
          Debug.Log("Pressed right click.");
        if(Input.GetMouseButtonDown(2))
          Debug.Log("Pressed middle click.");

        float movement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        if (grounded)
        {
            switch (transform.rotation.z)
            {
                case 0: //Ground
                    transform.position = new Vector2(transform.position.x + movement, transform.position.y);
                    break;
                case 90: //Left facing wall
                    transform.position = new Vector2(transform.position.x , transform.position.y + movement);
                    break;
                case 180: //Ceiling
                    transform.position = new Vector2(transform.position.x - movement, transform.position.y);
                    break;
                case 270: //Right facing wall
                    transform.position = new Vector2(transform.position.x , transform.position.y + movement);
                    break;
            }

        }
        if (Input.GetKeyDown("left") || Input.GetKeyDown("a"))
        {
            Debug.Log("left");
            float h = Input.GetAxis("Horizontal");
            this.GetComponent<SpriteRenderer>().flipX = true;

        }
        if (Input.GetKeyDown("right") || Input.GetKeyDown("d"))
        {
            Debug.Log("right");
            this.GetComponent<SpriteRenderer>().flipX = false;

        }
        if (timer < 0)
        {
            grounded = IsGrounded();
        }
        else
        {
            grounded = false;
        }



    }

    public bool IsGrounded()
    {
        float extra = .1f;
        RaycastHit2D raycast = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size,transform.rotation.z, transform.TransformDirection(Vector3.forward),  extra, playformLayer);
        Color rayColor;
        if (raycast.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(coll.bounds.center + new Vector3(coll.bounds.extents.x,0), Vector2.down * (coll.bounds.extents.y + extra), rayColor);
        Debug.DrawRay(coll.bounds.center - new Vector3(coll.bounds.extents.x, 0), Vector2.down * (coll.bounds.extents.y + extra), rayColor);
        Debug.DrawRay(coll.bounds.center + new Vector3(coll.bounds.extents.x, coll.bounds.extents.y + extra), Vector2.right * (coll.bounds.extents.x), rayColor);
        Debug.DrawRay(coll.bounds.center - new Vector3(coll.bounds.extents.x, coll.bounds.extents.y + extra), Vector2.right * (coll.bounds.extents.x), rayColor);
        Debug.Log(raycast.collider);
        return raycast.collider != null;

    }

    void OnCollisionExit2D(Collision2D collision)
    {

    }
}
