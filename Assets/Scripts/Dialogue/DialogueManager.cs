using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;
    private Queue<string> names;
    private Queue<string> sentences;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
      names = new Queue<string>();
      sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
      Debug.Log("Starting conversation with "  + dialogue.name[0]);
      animator.SetBool("OnScreen", true);
      nameText.text = dialogue.name[0];

      sentences.Clear();
      //fills queue with sentences from dialogue
      foreach (string sentence in dialogue.sentences){
        sentences.Enqueue(sentence);
      }
      foreach (string name in dialogue.name){
        names.Enqueue(name);
      }
      SoundManager s = GameObject.Find("SoundManager").GetComponent<SoundManager>();
      s.PlaySound("Whoosh", false);
      DisplayNextSentence();
    }

    public void DisplayNextSentence(){
      if(sentences.Count ==0 ){
          EndDialogue();
          return;
      }
      if(names.Count !=0 ){
        string name = names.Dequeue();
        nameText.text = name;
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
