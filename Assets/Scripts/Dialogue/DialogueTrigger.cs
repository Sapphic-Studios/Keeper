using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool Triggered = false;
    public Dialogue dialogue;

    public void TriggerDialogue(){
      if (!Triggered){
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        Triggered = true;
      }
    }

}
