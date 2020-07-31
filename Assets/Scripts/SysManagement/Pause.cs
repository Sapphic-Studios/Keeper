using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject UI;
    public GameObject optionsMenu;
    bool isPaused, inOptions;
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
                Cursor.visible = false;
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
        //UI.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Options()
    {
        if (inOptions)
        {
            inOptions = false;
            pauseMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }
        else
        {
            inOptions = true;
            pauseMenu.SetActive(false);
            optionsMenu.SetActive(true);
        }
    }
    public void RestartLevel()
    {
        pauseMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
