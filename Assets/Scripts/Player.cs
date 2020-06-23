using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 grav;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
      }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject wall = collision.gameObject;
        
        //grav = new Vector2(0, 0);
        //Debug.Log("name is " + wall + "and rotation is " + euler.z);
        
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}
