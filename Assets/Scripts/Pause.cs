using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject UI;
    bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        //pauseMenu = GameObject.FindWithTag("Pause");
    }   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                isPaused = true;
                pauseMenu.SetActive(true);
                UI.SetActive(false);
                Time.timeScale = 0f;
            }
        }
    }
    public void Resume()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        UI.SetActive(true);
        Time.timeScale = 1f;
    }
}
