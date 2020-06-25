using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Platform : MonoBehaviour
{
    GameObject player;
    Player script;
    Rigidbody2D rb;
    public float hitTimer;
    public bool up, left, right, down;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        script = player.GetComponent<Player>();
        rb = player.GetComponent<Rigidbody2D>();

        Vector3 euler = transform.rotation.eulerAngles;
        Quaternion rot = Quaternion.Euler(0, 0, euler.z);
        int angle = (int)euler.z;
        switch (angle)
        {
            case 0: //Ground
                up = true;
                break;
            case 90: //Left facing wall
                left = true;
                break;
            case 180: //Ceiling
                down = true;
                break;
            case 270: //Right facing wall
                right = true;
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hitTimer -= 0.01f;
        if (script.grounded)
        {
            if (hitTimer > 0)
            {
                rb.velocity = new Vector2(0, 0);
            }
            
            Vector3 euler = transform.rotation.eulerAngles;
            Quaternion rot = Quaternion.Euler(0, 0, euler.z);
            player.transform.rotation = rot;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hit");
       
        if (collision.gameObject.tag == "Player" && !script.grounded)
        {
            hitTimer = 0.2f;
            //script.grounded = true;
            rb.velocity = new Vector2(0f, 0f);
            
            Vector3 euler = transform.rotation.eulerAngles;
            Quaternion rot = Quaternion.Euler(0, 0, euler.z);
            player.transform.rotation = rot;
            int angle = (int)euler.z;
            switch (angle)
            {
                case 0: //Ground
                    Debug.Log("On Ground");
                    break;
                case 90: //Left facing wall
                    Debug.Log("On Left Wall");
                    break;
                case 180: //Ceiling
                    Debug.Log("On Ceiling");
                    break;
                case 270: //Right facing wall
                    Debug.Log("On Right Wall");
                    break;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("on");

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //script.grounded = false;
            int rot = (int)transform.rotation.z;
            script.grav = new Vector2(0, 0);
            rb.gravityScale = 0;
        }
    }
}
