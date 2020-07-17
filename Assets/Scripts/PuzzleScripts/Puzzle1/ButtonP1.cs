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
    private float t = 2f;
    SoundManager sound;


    void Start(){
       if(on)
        SR.sprite = onsprite;
       else
        SR.sprite = offsprite;
       sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
      t = t + Time.deltaTime;
      if(Input.GetKeyDown("e") && touchingplayer && t >= 1){ //limit speed of touching switch
        t = 0;
        on = !on;
        sound.PlaySound("Light", false);
        if (on)
         SR.sprite = onsprite;
        else
         SR.sprite = offsprite;
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
