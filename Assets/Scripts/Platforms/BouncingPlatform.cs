using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingPlatform : MonoBehaviour
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
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 euler = transform.rotation.eulerAngles;
            Quaternion rot = Quaternion.Euler(0, 0, euler.z);
            int angle = (int)euler.z;
            if (up || down)
            {
                rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
            }
            if (right || left)
            {
                rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);

            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //moving = true;
            collision.collider.transform.SetParent(null);
        }
    }


}
