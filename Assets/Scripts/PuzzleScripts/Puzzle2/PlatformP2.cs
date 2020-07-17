using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformP2 : MonoBehaviour
{

    [SerializeField]
    public string platformColor;
//-------------------
    private GameObject GameManager;
    private DoorP2 Door; //unfortunately non-descriptive name

    // Start is called before the first frame update
    void Start()
    {
      GameManager = GameObject.Find("GameManager");
      Door = GameManager.GetComponent<DoorP2>();
    }

    // Update is called once per frame
    void Update(){

    }


    private void OnCollisionEnter2D(Collision2D collision){
      if (collision.gameObject.tag == "Player"){
        //Door.AddtoArray(platformColor);
      }
    }

    private void addtoDoorArray(){
      //Door.AddtoArray(platformColor);
    }

}
