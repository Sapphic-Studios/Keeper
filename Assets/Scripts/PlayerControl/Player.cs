using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    private DistanceJoint2D joint;
    public Animator animator;
    public DialogueManager DM;
    public GameObject platform;
    public Vector2 grav;
    public Quaternion rot;
    public float timer;
    private float speed = 5f;
    public bool grounded;
    public CapsuleCollider2D coll;
    float extra = .1f;
    public Vector3 velocity;
    public SoundManager sound;
    public float angle;
    Collider2D col;
    [SerializeField] public LayerMask platformLayer;
    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<DistanceJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        DM = FindObjectOfType<DialogueManager>();
        sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        col = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
      animator.SetFloat("speed", Mathf.Abs((rb.velocity.x + rb.velocity.y)));
      animator.SetBool("grounded", IsGrounded() );
      animator.SetBool("walking", Input.GetButton("a") || Input.GetButton("d"));

      //handles advancing dialogue via DialogueManager
      if(Input.GetKeyDown("z")){
          DM.DisplayNextSentence();
      }

      if(Input.GetKeyDown("e")){
        //interact();
      }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= 0.01f;
        transform.rotation = rot;

        if (Input.GetMouseButtonDown(0)){
            Debug.Log("Pressed left click.");
            col.enabled = false;
            timer = 0.05f;
        }
        if (timer <= 0)
        {
            col.enabled = true;
        }
        else if(Input.GetMouseButtonDown(1)){
          Debug.Log("Pressed right click.");
          Debug.Log("mouse click position: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));


          //Debug.DrawRay(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), Color.green);
          //RaycastHit2D hit = Physics2D.Raycast(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
          //Debug.Log("x: " + hit.transform.position.x + ", y: " + hit.transform.position.y + ", z: " + hit.transform.position.z );
        }
        else if(Input.GetMouseButtonDown(2)){
          Debug.Log("Pressed middle click.");
        }

        float movement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        grounded = IsGrounded();
        if (grounded)
        {
            if((int)rot.eulerAngles.z != 180){
                if (atLeftEdge() && movement < 0f) movement = 0f;
                if (atRightEdge() && movement > 0f) movement = 0f;
            }
            else
            {
                if (atLeftEdge() && movement > 0f) movement = 0f;
                if (atRightEdge() && movement < 0f) movement = 0f;
            }

            //Debug.Log(Mathf.RoundToInt(transform.rotation.eulerAngles.z));
            if (movement != 0)
            {
                sound.PlaySound("Step",false);
                animator.SetBool("walking", true);
            }
            if (movement == 0f && timer<0) rb.velocity = new Vector2(0f, 0f);
            switch (Mathf.RoundToInt(transform.rotation.eulerAngles.z))
            {

                case 0: //Ground
                    //Debug.Log("Ground");
                    transform.position = new Vector2(transform.position.x + movement, transform.position.y);

                    break;
                case 90: //Left facing wall
                    //Debug.Log("Left facing wall");
                    transform.position = new Vector2(transform.position.x , transform.position.y + movement);
                    break;
                case 180: //Ceiling
                    //Debug.Log("Ceiling");
                    transform.position = new Vector2(transform.position.x + movement, transform.position.y);
                    break;
                case 270: //Right facing wall
                    //Debug.Log("Right facing wall");
                    transform.position = new Vector2(transform.position.x , transform.position.y - movement);
                    break;
            }

        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
        if (Input.GetKeyDown("left") || Input.GetKeyDown("a"))
        {
            //animator.SetBool("walking", true);
            //Debug.Log("left");
            if ((int)rot.eulerAngles.z == 180) this.GetComponent<SpriteRenderer>().flipX = false;
            else this.GetComponent<SpriteRenderer>().flipX = true;


        }
        if (Input.GetKeyDown("right") || Input.GetKeyDown("d"))
        {
            //animator.SetBool("walking", true);
            //Debug.Log("right");
            if ((int)rot.eulerAngles.z == 180) this.GetComponent<SpriteRenderer>().flipX = true;
            else this.GetComponent<SpriteRenderer>().flipX = false;
        }
        if ((int)rot.eulerAngles.z == 180) this.GetComponent<SpriteRenderer>().flipX = true;
    }

    public bool IsGrounded()
    {

        RaycastHit2D raycast = Physics2D.Raycast(coll.bounds.center, -transform.up, Mathf.Max(coll.bounds.extents.x, coll.bounds.extents.y) + extra, platformLayer);
        //RaycastHit2D raycast = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size,transform.rotation.z, transform.TransformDirection(Vector3.down),  extra, playformLayer);
        Color rayColor;
        if (raycast.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(coll.bounds.center, -transform.up * Mathf.Max(coll.bounds.extents.x, coll.bounds.extents.y), rayColor);
        /*
        Debug.DrawRay(coll.bounds.center + new Vector3(coll.bounds.extents.x,0), Vector2.down * (coll.bounds.extents.y + extra), rayColor);
        Debug.DrawRay(coll.bounds.center - new Vector3(coll.bounds.extents.x, 0), Vector2.down * (coll.bounds.extents.y + extra), rayColor)
        Debug.DrawRay(coll.bounds.center - new Vector3(coll.bounds.extents.x, coll.bounds.extents.y + extra), Vector2.right * (coll.bounds.extents.x) * 2, rayColor);
        */
        //Debug.Log(raycast.collider);
        return raycast.collider != null;

    }
    public bool atLeftEdge()
    {
        RaycastHit2D raycast;
        Color rayColor = Color.yellow;
        Vector3 shift = new Vector3(0,0,0);
        switch ((int)rot.eulerAngles.z)
        {
            case 0: //Ground
                //Debug.Log("Ground");
                shift = coll.bounds.center - new Vector3(coll.bounds.extents.x, 0);


                break;
            case 90: //Left facing wall
                //Debug.Log("Left facing wall");
                shift = coll.bounds.center - new Vector3(0, coll.bounds.extents.y);

                break;
            case 180: //Ceiling
                //Debug.Log("Ceiling");
                shift = coll.bounds.center + new Vector3(coll.bounds.extents.x, 0);

                break;
            case 270: //Right facing wall
                //Debug.Log("Right facing wall");
                shift = coll.bounds.center + new Vector3(0, coll.bounds.extents.y);

                break;
        }
        raycast = Physics2D.Raycast(shift, -transform.up, Mathf.Max(coll.bounds.extents.x, coll.bounds.extents.y) + extra, platformLayer);
        if (raycast.collider != null)
        {
            rayColor = Color.yellow;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(shift, -transform.up * Mathf.Max(coll.bounds.extents.x, coll.bounds.extents.y), rayColor);
        //RaycastHit2D raycast = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size,transform.rotation.z, transform.TransformDirection(Vector3.down),  extra, playformLayer);


        return raycast.collider == null;
    }
    public bool atRightEdge()
    {
        RaycastHit2D raycast;
        Color rayColor = Color.yellow;
        Vector3 shift = new Vector3(0, 0, 0);
        switch ((int)rot.eulerAngles.z)
        {
            case 0: //Ground
                //Debug.Log("Ground");
                shift = coll.bounds.center + new Vector3(coll.bounds.extents.x, 0);


                break;
            case 90: //Left facing wall
                //Debug.Log("Left facing wall");
                shift = coll.bounds.center + new Vector3(0, coll.bounds.extents.y);

                break;
            case 180: //Ceiling
                //Debug.Log("Ceiling");
                shift = coll.bounds.center - new Vector3(coll.bounds.extents.x, 0);

                break;
            case 270: //Right facing wall
                //Debug.Log("Right facing wall");
                shift = coll.bounds.center - new Vector3(0, coll.bounds.extents.y);

                break;
        }
        raycast = Physics2D.Raycast(shift, -transform.up, Mathf.Max(coll.bounds.extents.x, coll.bounds.extents.y) + extra, platformLayer);
        if (raycast.collider != null)
        {
            rayColor = Color.yellow;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(shift, -transform.up * Mathf.Max(coll.bounds.extents.x, coll.bounds.extents.y), rayColor);
        return raycast.collider == null;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

    }

}
