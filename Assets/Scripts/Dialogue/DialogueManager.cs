using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;
    private Queue<string> sentences;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
      sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
      Debug.Log("Starting conversation with "  + dialogue.name);
      animator.SetBool("OnScreen", true);
      nameText.text = dialogue.name;

      sentences.Clear();
      //fills queue with sentences from dialogue
      foreach (string sentence in dialogue.sentences){
        sentences.Enqueue(sentence);
      }
      DisplayNextSentence();
    }

    public void DisplayNextSentence(){
      if(sentences.Count ==0 ){
          EndDialogue();
          return;
      }
      string sentence = sentences.Dequeue();
      Debug.Log(sentence);
      StopAllCoroutines();
      StartCoroutine(TypeSentence(sentence));


    }

    IEnumerator TypeSentence(string sentence)
    {
      dialogueText.text = "";
      foreach (char letter in sentence.ToCharArray()){
        dialogueText.text += letter;
        yield return null;
      }
    }

    void EndDialogue(){
      Debug.Log("End of Dialogue");
      animator.SetBool("OnScreen", false);
    }


}
