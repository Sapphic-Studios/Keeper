using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dialogueBox;
    public GameObject Name;
    public GameObject TextBox;
    public GameObject Choice1;
    public GameObject Choice2;
    bool onScreen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("n"))
        {
            if (onScreen)
            {
                onScreen = false;
                dialogueBox.SetActive(false);
                
            }
            else
            {
                onScreen = true;
                dialogueBox.SetActive(true);
            }
        }
    }
}
