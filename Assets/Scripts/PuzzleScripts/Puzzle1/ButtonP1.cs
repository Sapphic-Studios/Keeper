using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonP1 : MonoBehaviour
{
    [SerializeField]
    public SpriteRenderer SR;
    [SerializeField]
    public bool on = true;
    [SerializeField]
    private Sprite onsprite;
    [SerializeField]
    private Sprite offsprite;
    //-------------
    private Collider2D playercollission;
    public bool touchingplayer = false;
    private float t = 0f;



    void Start(){
       if(on)
        SR.sprite = onsprite;
       else
        SR.sprite = offsprite;
    }

    // Update is called once per frame
    void Update()
    {
      t = t + Time.deltaTime;
      if(Input.GetKeyDown("e") && touchingplayer && t >= 2){ //limit speed of touching switch
        t = 0;
        on = !on;
      }


    }


    private void OnTriggerEnter2D(Collider2D collision){
      Debug.Log("IT WAS TOUCHED");
      if (collision.gameObject.tag == "Player"){
        touchingplayer = true;
        playercollission = collision;
      }
    }

    private void  OnTriggerExit2D(Collider2D collision)
    {
      Debug.Log("IT WAS LEFT");
      if (collision.gameObject.tag == "Player"){
        touchingplayer = false;
        playercollission = null;
      }
    }


}
