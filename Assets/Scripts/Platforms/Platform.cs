using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Platform : MonoBehaviour
{
    GameObject player;
    Player script;
    Rigidbody2D rb;
    TilemapCollider2D coll;
    public enum Direction {up,down,left,right};
    public Direction dir;
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
        coll = GetComponent<TilemapCollider2D>();



        euler = transform.rotation.eulerAngles;
        rot = Quaternion.Euler(0, 0, euler.z);
        int angle = (int)euler.z;
        switch (angle)
        {
            case 0: //Ground
                dir = Direction.up;
                up = true;
                break;
            case 90: //Left facing wall
                left = true;
                dir = Direction.left;
                break;
            case 180: //Ceiling
                dir = Direction.down;
                down = true;
                break;
            case 270: //Right facing wall
                dir = Direction.right;
                right = true;
                break;
        }
    }

    void FixedUpdate()
    {
        hitTimer -= 0.01f;
        dir = getDirection();
        if (script.grounded)
        {
            /*
            if (hitTimer < 0)
            {
                rb.velocity = new Vector2(0, 0);
            }

            Vector3 euler = transform.rotation.eulerAngles;
            Quaternion rot = Quaternion.Euler(0, 0, euler.z);
            player.transform.rotation = rot;*/
        }
    }

    bool directionHit(Direction d){
        switch (d)
        {
            case Direction.up:
                raycast = Physics2D.Raycast(script.coll.bounds.center, -Vector3.up, Mathf.Max(script.coll.bounds.extents.x, script.coll.bounds.extents.y) + 1f, script.platformLayer);
                break;
            case Direction.right:
                raycast = Physics2D.Raycast(script.coll.bounds.center, -Vector3.right, Mathf.Max(script.coll.bounds.extents.x, script.coll.bounds.extents.y) + 1f, script.platformLayer);
                break;
            case Direction.down:
                raycast = Physics2D.Raycast(script.coll.bounds.center, Vector3.up, Mathf.Max(script.coll.bounds.extents.x, script.coll.bounds.extents.y) + 1f, script.platformLayer);
                break;
            case Direction.left:
                raycast = Physics2D.Raycast(script.coll.bounds.center, Vector3.right, Mathf.Max(script.coll.bounds.extents.x, script.coll.bounds.extents.y) + 1f, script.platformLayer);
                break;
        }
        //Debug.Log(raycast.collider);
        return raycast.collider != null;
    }

    Direction getDirection()
    {
        float extend = .1f;
        float extra = 1f;
        rayUp = Physics2D.Raycast(script.coll.bounds.center, -Vector3.up, Mathf.Max(script.coll.bounds.extents.x, script.coll.bounds.extents.y) + extend, script.platformLayer);
        rayRight = Physics2D.Raycast(script.coll.bounds.center, -Vector3.right, Mathf.Max(script.coll.bounds.extents.x, script.coll.bounds.extents.y) + extend, script.platformLayer);
        rayDown = Physics2D.Raycast(script.coll.bounds.center, Vector3.up, Mathf.Max(script.coll.bounds.extents.x, script.coll.bounds.extents.y) + extend, script.platformLayer);
        rayLeft = Physics2D.Raycast(script.coll.bounds.center, Vector3.right, Mathf.Max(script.coll.bounds.extents.x, script.coll.bounds.extents.y) + extend, script.platformLayer);

        Color rayColor = Color.red;
        if (rayUp.collider != null) rayColor = Color.green; 
        if (rayRight.collider != null) rayColor = Color.green; 
        if (rayDown.collider != null) rayColor = Color.green; 
        if (rayLeft.collider != null) rayColor = Color.green; 

        Debug.DrawRay(script.coll.bounds.center, -Vector3.up * Mathf.Max(script.coll.bounds.extents.x, script.coll.bounds.extents.y), rayColor);
        Debug.DrawRay(script.coll.bounds.center, -Vector3.right * Mathf.Max(script.coll.bounds.extents.x, script.coll.bounds.extents.y), rayColor);
        Debug.DrawRay(script.coll.bounds.center, Vector3.up * Mathf.Max(script.coll.bounds.extents.x, script.coll.bounds.extents.y), rayColor);
        Debug.DrawRay(script.coll.bounds.center, Vector3.right * Mathf.Max(script.coll.bounds.extents.x, script.coll.bounds.extents.y), rayColor);
        if (rayUp.collider != null)  return Direction.up;
        if (rayRight.collider != null) return Direction.right;
        if (rayDown.collider != null) return Direction.down;
        if (rayLeft.collider != null) return Direction.left;

         right45 = Physics2D.Raycast(script.coll.bounds.center + player.transform.up, (player.transform.up + player.transform.right).normalized, extra, script.platformLayer);
        left45 = Physics2D.Raycast(script.coll.bounds.center + player.transform.up, (player.transform.up - player.transform.right).normalized, extra, script.platformLayer);
        if (right45.collider != null) rayColor = Color.green;
        if (left45.collider != null) rayColor = Color.green;

        Debug.DrawRay(script.coll.bounds.center + player.transform.up, (player.transform.up + player.transform.right).normalized * extra, rayColor);
        Debug.DrawRay(script.coll.bounds.center + player.transform.up, (player.transform.up - player.transform.right).normalized * extra, rayColor);

        if (script.velocity.x > 0 && script.velocity.y > 0 && right45.collider == null && left45.collider != null) return Direction.down;
        if (script.velocity.x > 0 && script.velocity.y > 0 && right45.collider != null && left45.collider == null) return Direction.left;
        if (script.velocity.x > 0 && script.velocity.y < 0 && right45.collider == null && left45.collider != null) return Direction.left;
        if (script.velocity.x > 0 && script.velocity.y < 0 && right45.collider != null && left45.collider == null) return Direction.up;
        if (script.velocity.x < 0 && script.velocity.y < 0 && right45.collider == null && left45.collider != null) return Direction.up;
        if (script.velocity.x < 0 && script.velocity.y < 0 && right45.collider != null && left45.collider == null) return Direction.right;
        if (script.velocity.x < 0 && script.velocity.y > 0 && right45.collider == null && left45.collider != null) return Direction.right;
        if (script.velocity.x < 0 && script.velocity.y > 0 && right45.collider != null && left45.collider == null) return Direction.down;
        /*
        Vector3 shift = new Vector3(0, 0, 0);
        shift = script.coll.bounds.center + new Vector3(2f, 2f);
        rayWup = Physics2D.Raycast(shift, -Vector3.right, 4f, script.platformLayer);
        Debug.DrawRay(shift, -Vector3.right * 4f, rayColor);

        shift = script.coll.bounds.center + new Vector3(2f, -2f);
        rayWright = Physics2D.Raycast(shift, Vector3.up, 4f, script.platformLayer);
        Debug.DrawRay(shift, Vector3.up * 4f, rayColor);

        shift = script.coll.bounds.center + new Vector3(-2f, -2f);
        rayWdown = Physics2D.Raycast(shift, Vector3.right, 4f, script.platformLayer);
        Debug.DrawRay(shift, Vector3.right * 4f, rayColor);

        shift = script.coll.bounds.center + new Vector3(-2f, 2f);
        rayWleft = Physics2D.Raycast(shift, -Vector3.up, 4f, script.platformLayer);
        Debug.DrawRay(shift, -Vector3.up * 4f, rayColor);
        if (rayWup.collider != null || rayWright.collider != null || rayWdown.collider != null || rayWleft.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        if (rayWup.collider != null) return Direction.up;
        if (rayWright.collider != null) return Direction.right;
        if (rayWdown.collider != null) return Direction.down;
        if (rayWleft.collider != null) return Direction.left;

        //player.transform.position = pos;
        //Debug.Log(raycast.collider);*/
        return Direction.up;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hit");

        if (collision.gameObject.tag == "Player")
        {
            script.platform = this.gameObject;

            ContactPoint2D contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector2.up, contact.normal);
            pos = contact.point;
            
            Vector3 direction = player.transform.position - transform.position;
            direction = player.transform.InverseTransformDirection(direction);
            float ang = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            script.angle = ang;
            hitTimer = 0.2f;
            //script.grounded = true;
            Debug.Log(getDirection());
            switch (getDirection())
            {
                case Direction.up:
                    rot = Quaternion.Euler(0, 0, 0);
                    if (rayUp.collider == null && rayRight.collider == null && rayDown.collider == null && rayLeft.collider == null) player.transform.position = new Vector2(pos.x, player.transform.position.y) ;
                    break;
                case Direction.right:
                    rot = Quaternion.Euler(0, 0, 270);
                    if (rayUp.collider == null && rayRight.collider == null && rayDown.collider == null && rayLeft.collider == null) player.transform.position = new Vector2(player.transform.position.x,pos.y );
                    break;
                case Direction.down:
                    rot = Quaternion.Euler(0, 0, 180);
                    if (rayUp.collider == null && rayRight.collider == null && rayDown.collider == null && rayLeft.collider == null) player.transform.position = new Vector2(pos.x, player.transform.position.y);
                    break;
                case Direction.left:
                    rot = Quaternion.Euler(0, 0, 90);
                    if (rayUp.collider == null && rayRight.collider == null && rayDown.collider == null && rayLeft.collider == null) player.transform.position = new Vector2(player.transform.position.x, pos.y);
                    break;
            }
            



            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            int angle = (int)euler.z;
            script.sound.PlaySound("Step", true);
            rb.velocity = new Vector2(0f, 0f);

            player.transform.rotation = rot;
            /*
            rayUp = Physics2D.Raycast(script.coll.bounds.center, -Vector3.up, Mathf.Max(script.coll.bounds.extents.x, script.coll.bounds.extents.y) + 1f, script.platformLayer);
            rayRight = Physics2D.Raycast(script.coll.bounds.center, -Vector3.right, Mathf.Max(script.coll.bounds.extents.x, script.coll.bounds.extents.y) + 1f, script.platformLayer);
            rayDown = Physics2D.Raycast(script.coll.bounds.center, Vector3.up, Mathf.Max(script.coll.bounds.extents.x, script.coll.bounds.extents.y) + 1f, script.platformLayer);
            rayLeft = Physics2D.Raycast(script.coll.bounds.center, Vector3.right, Mathf.Max(script.coll.bounds.extents.x, script.coll.bounds.extents.y) + 1f, script.platformLayer);
            if (dir==Direction.up && rayDown.collider!=null) rot = Quaternion.Euler(0, 0, 180);
            if (dir == Direction.down && rayDown.collider != null) rot = Quaternion.Euler(0, 0, 0);
            if (dir == Direction.left && rayLeft.collider != null) rot = Quaternion.Euler(0, 0, 90);
            if (dir == Direction.right && rayRight.collider != null) rot = Quaternion.Euler(0, 0, 270);*/
            script.rot = rot;
            //rb.velocity = new Vector2(0f, 0f); script.rot = rot;
            /*
            if (directionHit(dir))
            {
                script.sound.PlaySound("Step", true);
                rb.velocity = new Vector2(0f, 0f);
                script.rot = rot;
            }*/
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //script.rot = rot;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
    }
}
