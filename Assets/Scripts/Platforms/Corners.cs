using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corners : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    Player script;
    Rigidbody2D rb;
    BoxCollider2D coll;
    public enum Direction { up, down, left, right };
    public Direction dir,sendDir;
    Vector3 euler;
    public Quaternion rot;
    public float hitTimer;
    public bool up, left, right, down;
    RaycastHit2D raycast;
    RaycastHit2D rayUp, rayDown, rayLeft, rayRight, rayWup, rayWdown, rayWleft, rayWright, right45, left45;
    Vector3 pos;
    //Ray2D rayUp, rayDown, rayLeft, rayRight;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        script = player.GetComponent<Player>();
        rb = player.GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();



        euler = transform.rotation.eulerAngles;
        rot = Quaternion.Euler(0, 0, euler.z);
        int angle = (int)euler.z;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !script.grounded)
        {
            /*
            switch(sendDir)
            {
                case Direction.up:
                    player.transform.position += Vector3.up * 0.5f;
                    break;
                case Direction.right:
                    rot = Quaternion.Euler(0, 0, 270);
                //if (rayUp.collider == null && rayRight.collider == null && rayDown.collider == null && rayLeft.collider == null) player.transform.position = new Vector2(player.transform.position.x,pos.y );
                break;
                case Direction.down:
                    rot = Quaternion.Euler(0, 0, 180);
                //if (rayUp.collider == null && rayRight.collider == null && rayDown.collider == null && rayLeft.collider == null) player.transform.position = new Vector2(pos.x, player.transform.position.y);
                break;
                case Direction.left:
                    rot = Quaternion.Euler(0, 0, 90);
                //if (rayUp.collider == null && rayRight.collider == null && rayDown.collider == null && rayLeft.collider == null) player.transform.position = new Vector2(player.transform.position.x, pos.y);
                break;
            }*/
            
            switch (dir)
            {
                case Direction.up:
                    rot = Quaternion.Euler(0, 0, 0);
                    break;
                case Direction.right:
                    rot = Quaternion.Euler(0, 0, 270);
                    //if (rayUp.collider == null && rayRight.collider == null && rayDown.collider == null && rayLeft.collider == null) player.transform.position = new Vector2(player.transform.position.x,pos.y );
                    break;
                case Direction.down:
                    rot = Quaternion.Euler(0, 0, 180);
                    //if (rayUp.collider == null && rayRight.collider == null && rayDown.collider == null && rayLeft.collider == null) player.transform.position = new Vector2(pos.x, player.transform.position.y);
                    break;
                case Direction.left:
                    rot = Quaternion.Euler(0, 0, 90);
                    //if (rayUp.collider == null && rayRight.collider == null && rayDown.collider == null && rayLeft.collider == null) player.transform.position = new Vector2(player.transform.position.x, pos.y);
                    break;
            }

            script.rot = rot;
            player.transform.position = transform.position;
            //script.sound.PlaySound("Step", true);
            rb.velocity = new Vector2(0f, 0f);
        }
     }
}
