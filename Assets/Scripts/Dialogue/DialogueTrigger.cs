using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool Triggered = false;
    public bool leaveAfterTalk = false;
    public Dialogue dialogue;

    public void TriggerDialogue(){
      if (!Triggered){
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, leaveAfterTalk, this.name);
        Triggered = true;
      }
    }

}
