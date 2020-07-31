using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float SpeedX = 5;
    [SerializeField] private float SpeedY = 5;
    [SerializeField] private float LoopTimerX = 2f;
    [SerializeField] private float LoopTimerY = 2f;
    [SerializeField] private float delay = 0f;
    private float ActualSpeedX = 0f;
    private float ActualSpeedY = 0f;
    private float t_x = 0f;
    private float t_y = 0f;

    //private Vector3 starting_position;
    [SerializeField]
    private bool changeDirection = false;


    // Start is called before the first frame update
    void Start()
    {
      t_x = LoopTimerX;
      t_y = LoopTimerY;
      ActualSpeedX = SpeedX;
      ActualSpeedY = SpeedY;

    }

    // Update is called once per frame
    void Update()
    {
      if(delay > 0){
        delay = delay - Time.deltaTime;
      }
      else{

        t_x = t_x - Time.deltaTime;
        t_y = t_y - Time.deltaTime;

        if( t_x <= 0 ){
          ActualSpeedX = SpeedX * Mathf.Sign(SpeedX) * Mathf.Sign(ActualSpeedX) * -1;
          t_x = LoopTimerX;
          if (changeDirection)
            transform.localScale = new Vector3( ( -1 * (transform.localScale.x) ), (transform.localScale.y), transform.localScale.z);
        }
        if( t_y <= 0 ){
          ActualSpeedY = SpeedY * Mathf.Sign(SpeedY) * Mathf.Sign(ActualSpeedY) * -1;
          t_y = LoopTimerY;
        }
        transform.position = new Vector3( (transform.position.x + (ActualSpeedX * Time.deltaTime)), (transform.position.y + (ActualSpeedY * Time.deltaTime)), transform.position.z);

      }
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
