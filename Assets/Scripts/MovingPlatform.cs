using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float SpeedX = 1;
    [SerializeField] private float SpeedY = 1;
    private Vector3 starting_position;
    //public bool going_right;
    //public bool going_up;

    // Start is called before the first frame update
    void Start()
    {
      starting_position = transform.position;
      Debug.Log("starting postion: " + starting_position);
      Debug.Log("x: " + starting_position.x);
      Debug.Log("y: " + starting_position.y);
      Debug.Log("z: " + starting_position.z);

    }

    // Update is called once per frame
    void Update()
    {
      transform.position = new Vector3( (transform.position.x + (SpeedX * Time.deltaTime)), (transform.position.y + (SpeedY * Time.deltaTime)), transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision){
      if (collision.gameObject.tag == "Player"){
        //moving = true;
        collision.collider.transform.SetParent(transform);
      }
    }

    private void OnCollisionExit2D(Collision2D collision){
      if (collision.gameObject.tag == "Player"){
        //moving = true;
        collision.collider.transform.SetParent(null);
      }
    }
}
