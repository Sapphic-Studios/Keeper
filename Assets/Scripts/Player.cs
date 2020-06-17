using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
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
        if (Input.GetMouseButtonDown(0))
        {
            timer = 0.2f;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        GameObject wall = collision.gameObject;
        rb.velocity = new Vector2(0f, 0f);
        Vector3 euler = wall.transform.rotation.eulerAngles;
        Quaternion rot = Quaternion.Euler(0, 0, euler.z);
        transform.rotation = rot;
        print("name is " + wall + "and rotation is " + euler.z);
        print("hit");
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (timer < 0) rb.velocity = new Vector2(0f, 0f);
    }
}
