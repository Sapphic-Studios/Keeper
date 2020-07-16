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

    int newCount;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
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
        SceneManager.LoadScene(scene);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
