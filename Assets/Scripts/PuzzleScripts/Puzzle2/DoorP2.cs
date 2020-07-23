using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoorP2 : MonoBehaviour
{
    [SerializeField]
    public string[] PATTERN;
    [SerializeField]
    public GameObject door;
//--------------------------------
    private string[] PLAYERINPUT;
    private int index = 0;
    private int arraylength;
    private bool isCorrect = true;
    private bool unlocked = false;


    // Start is called before the first frame update
    void Start(){
      arraylength = PATTERN.Length;
      PLAYERINPUT = new string[arraylength];
      for (int i= 0; i < arraylength; i++){
          PLAYERINPUT[i] = "";
      }
    }

    // Update is called once per frame
    void Update(){
      if (!unlocked){
        isCorrect = true;
        for(int i = 0; i < arraylength; i++){
          if( !(PLAYERINPUT[i].Equals(PATTERN[i])) ){
            isCorrect = false;
          }
        }
        if(isCorrect){
          door.SetActive(false);
          unlocked=true;
        }
      }
    }

    public void AddtoArray(string platColor){
      bool new_color = true;
      if(index != arraylength){
        if(index == 0){
          PLAYERINPUT[index] = platColor;
          index += 1;
        }
        else{
          for (int i= 0; i < index; i++){
            if (PLAYERINPUT[i].Equals(platColor)){
              new_color = false;
            }
          }
          if(new_color){
            PLAYERINPUT[index] = platColor;
            index += 1;
          }
        }



      }
    }

    public void clearArray(){
      for (int i= 0; i < arraylength; i++){
          PLAYERINPUT[i] = "";
      }
      index = 0;
    }





}
