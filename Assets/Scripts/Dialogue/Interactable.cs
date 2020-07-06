using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    [SerializeField] public DialogueTrigger DT;



    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
      Debug.Log(collision.gameObject.name + " : " + gameObject.name + " : " + Time.time);
      if (collision.gameObject.tag == "Player"){
        DT.TriggerDialogue();
      }

    }

}
