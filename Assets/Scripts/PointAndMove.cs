using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndMove : MonoBehaviour
{
    private Vector3 target;
    public GameObject pointer;
    public GameObject player;
    public GameObject arrow;
    public float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        pointer.transform.position = new Vector2(target.x, target.y);

        Vector3 difference = target - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);


        if (Input.GetMouseButtonDown(0))
        {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            moveInDirection(direction, rotationZ);
        }
    }
    void moveInDirection(Vector2 direction, float rotationZ)
    {
        player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        player.GetComponent<Rigidbody2D>().velocity = direction * speed;
    }
}
