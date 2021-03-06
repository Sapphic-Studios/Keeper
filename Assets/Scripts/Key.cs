﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision){
      if (collision.gameObject.tag == "Player"){
        door.SetActive(false);
        SoundManager s = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        s.PlaySound("CollectChain", true);
        gameObject.SetActive(false);
      }
    }
}
