using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorP1 : MonoBehaviour
{

    [SerializeField]
    public ButtonP1[] BUTTONS;
    [SerializeField]
    public bool[] PATTERN;
//--------------------------------
    private bool isCorrect = true;
    private bool unlocked = false;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      if (!unlocked){
        isCorrect = true;
        for(int i=0; i < BUTTONS.Length; i++){
          Debug.Log(BUTTONS[i].on + " != " +PATTERN[i]);
          if(BUTTONS[i].on != PATTERN[i]){

            isCorrect = false;
          }
        }

        if(isCorrect){
          GameObject.Find("DOORBAD").SetActive(false);
          unlocked=true;
        }

      }
    }
}
