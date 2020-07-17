using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorP2 : MonoBehaviour
{
    [SerializeField]
    public string[] PATTERN;
    [SerializeField]
    public string objectname;
//--------------------------------
    private string[] PLAYERINPUT;
    private int index = 0;
    private bool isCorrect = true;
    private bool unlocked = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      /*if (!unlocked){
        isCorrect = true;
        for(int i=0; i < BUTTONS.Length; i++){
          Debug.Log(BUTTONS[i].on + " != " +PATTERN[i]);
          if(BUTTONS[i].on != PATTERN[i]){

            isCorrect = false;
          }
        }

        if(isCorrect){
          GameObject.Find(objectname).SetActive(false);
          unlocked=true;
        }

    }

    public void AddtoArray(string platColor){
      if(PLAYERINPUT[index] != platColor){
          PLAYERINPUT[index] != platColor;
          index +=1;
          if(index == PLAYERINPUT.Length){
            index =0;
          }
      }

      */
    }

}
