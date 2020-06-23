using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Platform : MonoBehaviour
{
    GameObject player;
    Player script;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        script = player.GetComponent<Player>();
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hit");
        if (collision.gameObject.tag == "Player")
        {
            
            rb.velocity = new Vector2(0f, 0f);
            rb.gravityScale = 10;
            
            Vector3 euler = transform.rotation.eulerAngles;
            Quaternion rot = Quaternion.Euler(0, 0, euler.z);
            player.transform.rotation = rot;
            int angle = (int)euler.z;
            switch (angle)
            {
                case 0: //Ground
                    script.grav = new Vector2(0, -1);
                    Debug.Log("On Ground");
                    break;
                case 90: //Left facing wall
                    script.grav = new Vector2(1, 0);
                    Debug.Log("On Left Wall");
                    break;
                case 180: //Ceiling
                    script.grav = new Vector2(0, 1);
                    Debug.Log("On Ceiling");
                    break;
                case 270: //Right facing wall
                    script.grav = new Vector2(-1, 0);
                    Debug.Log("On Right Wall");
                    break;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("on");
        if (script.timer>0)rb.velocity = new Vector2(0f, 0f);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int rot = (int)transform.rotation.z;
            script.grav = new Vector2(0, 0);
            rb.gravityScale = 0;
        }
    }
}
