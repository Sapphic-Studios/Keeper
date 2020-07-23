using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context : MonoBehaviour
{
    public GameObject contextMenu;
    // Start is called before the first frame update
    void Start()
    {
        //contextMenu = GameObject.Find("Context");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void context()
    {
        contextMenu.SetActive(true);
    }
}
