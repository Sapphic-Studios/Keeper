using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Text chainCount;
    GameObject player;
    public bool mute;
    
    SoundManager sound;

    int newCount;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CountUpdate(int count)
    {
        newCount = int.Parse(chainCount.text); // get current score
        newCount += count; // add 100 points to the score
        chainCount.text = newCount.ToString();
    }
    public void ChangeLevel(string scene)
    {
        Time.timeScale = 1f;
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, scene));
    }


    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void MuteSound()
    {
        sound.Mute();
    }
    public void ChangeVolume(float vol)
    {
        sound.Volume(vol);
    }
}
