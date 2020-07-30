using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndMove : MonoBehaviour
{
    private Vector3 target;
    public GameObject pointer;
    public GameObject player;
    public GameObject arrow;
    //public Collider2D playerhitbox;
    Player script;
    public float speed;
    private Transform pivot;
    public float radius = .2f;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        script = player.GetComponent<Player>();
        pivot = player.transform;
        arrow.transform.parent = pivot;
        transform.position += Vector3.up * radius;
    }

    // Update is called once per frame
    void Update()
    {

        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        pointer.transform.position = new Vector2(target.x, target.y);


        Vector3 difference = target - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg -90f;
        arrow.transform.rotation = Quaternion.AngleAxis(rotationZ + 90f, Vector3.forward);
        arrow.transform.position = player.transform.position + (Quaternion.AngleAxis(rotationZ, Vector3.forward * radius) * Vector3.up);
        float dist = Vector2.Distance(target, player.transform.position);
        speed = Mathf.Clamp( dist+3, 5.0f,8.0f);


        if (Input.GetMouseButtonDown(0) && player.GetComponent<Rigidbody2D>().velocity.x ==0)
        {

            if (!badAngle() )
            {
                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                direction.Normalize();
                //animator.SetBool("MarleyJump",true);
                StartCoroutine(moveInDirection(direction, rotationZ));
                script.grounded = false;
            }
        }
        if (script.grounded)
        {
            arrow.SetActive(true);
        }
        else
        {
            arrow.SetActive(false);
        }
    }
    bool badAngle()
    {
        switch ((int)script.rot.eulerAngles.z)
        {
            case 0: //Ground
                //Debug.Log("Ground");
                return (target.y < player.transform.position.y);
            case 90: //Left facing wall
                //Debug.Log("Left facing wall");
                return (target.x > player.transform.position.x);
            case 180: //Ceiling
                //Debug.Log("Ceiling");
                return (target.y > player.transform.position.y);
            case 270: //Right facing wall
                //Debug.Log("Right facing wall");
                return (target.x < player.transform.position.x);
        }
        return false;
    }

    IEnumerator moveInDirection(Vector2 direction, float rotationZ)
    {
        yield return new WaitForSeconds(0f);
        //player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        script.rot = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        player.GetComponent<Rigidbody2D>().velocity = direction * speed;
        script.velocity = player.GetComponent<Rigidbody2D>().velocity;
    }

}
