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
    public GameObject speaker;
    public Animator animator;
    public GameObject thoughtPrefab;
    bool hasSpawned;
    //keep track of the current coroutine
    private IEnumerator coroutine;
    private bool crRunning = false;

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
      hasSpawned = false;
      //clear out old queues
      names.Clear();
      sentences.Clear();
      //fills queues with names/sentences from dialogue
      foreach (string sentence in dialogue.sentences){
        sentences.Enqueue(sentence);
      }
      foreach (string name in dialogue.name){
        names.Enqueue(name);
      }
      SoundManager s = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        //SpawnBubble();
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
      if(crRunning) StopCoroutine(coroutine);
      coroutine = TypeSentence(sentence);
      StartCoroutine(coroutine);


    }

    IEnumerator TypeSentence(string sentence)
    {
      crRunning=true;
      dialogueText.text = "";
      foreach (char letter in sentence.ToCharArray()){
        dialogueText.text += letter;
        yield return null;
      }
      crRunning=false;
    }

    void EndDialogue(){
      Debug.Log("End of Dialogue");
      animator.SetBool("OnScreen", false);
    }
    private void SpawnBubble()
    {
        if (!hasSpawned)
        {
            GameObject bubble = Instantiate(thoughtPrefab, speaker.transform.position, transform.rotation);
            bubble.transform.parent = speaker.transform;
            hasSpawned = true;
        }

    }


}
