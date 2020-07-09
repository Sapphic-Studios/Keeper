using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    GameObject GameManager;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            GameManager.GetComponent<GameManager>().CountUpdate(1);
            SoundManager s = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            s.PlaySound("CollectChain", true);
            Destroy(gameObject);
        }

    }
}
