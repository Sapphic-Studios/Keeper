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
    TilemapCollider2D collider;
    public enum Direction {up,down,left,right};
    public Direction dir;
    Vector3 euler;
    Quaternion rot;
    public float hitTimer;
    public bool up, left, right, down;
    RaycastHit2D raycast;
    Ray2D rayUp, rayDown, rayLeft, rayRight;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        script = player.GetComponent<Player>();
        rb = player.GetComponent<Rigidbody2D>();
        collider = GetComponent<TilemapCollider2D>();



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

        RaycastHit2D hit;
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
        Debug.Log(raycast.collider);
        return raycast.collider != null;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hit");

        if (collision.gameObject.tag == "Player")
        {
            script.platform = this.gameObject;
            rayUp = new Ray2D(player.transform.position, Vector3.up);
            rayDown = new Ray2D(player.transform.position, Vector3.down);
            rayLeft = new Ray2D(player.transform.position, Vector3.left);
            rayRight = new Ray2D(player.transform.position, Vector3.right);

            hitTimer = 0.2f;
            //script.grounded = true;



            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            int angle = (int)euler.z;

            if (directionHit(dir))
            {
                rb.velocity = new Vector2(0f, 0f);
                script.rot = rot;
            }

            switch (angle)
            {
                case 0: //Ground
                    Debug.Log("On Ground");
                    //player.transform.position = new Vector3(player.transform.position.x,this.transform.position.y, player.transform.position.z);
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
        //script.rot = rot;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
    }
}
