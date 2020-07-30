using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager s = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        s.PlayWhisper(Random.Range(0, 5), transform.position,true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision){
      if (collision.gameObject.tag == "Player"){

        Scene scene = SceneManager.GetActiveScene();
            
            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.Out,scene.name));
            
            SceneManager.LoadScene(scene.name);
      }
    }
}
