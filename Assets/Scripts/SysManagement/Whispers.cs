using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using TMPro;
using UnityEngine.Audio;

public class Whispers : MonoBehaviour
{
    // Start is called before the first frame update
    public Sound[] sounds;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Sound findSound(int name)
    {
        Sound s = sounds[name];
        if (s == null)
        {
            Debug.Log("no sound");
            return null;
        }
        return s;
    }
}
