using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    float timer = 0.5f;
    public Sound[] sounds;
    private Dictionary<Sound, float> soundTimerDictionary;
    private void Awake()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        foreach (Sound s in sounds)
        {    
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            //ds.source.volume = s.volume;
            //as.source.pitch = s.pitch;
            soundTimerDictionary[s] = 0f; 
        }
        PlaySound("Step");
    }
    private void Update()
    {
        timer -= Time.deltaTime;
    }
    private bool CanPlaySound(Sound s)
    {

        
        if (soundTimerDictionary.ContainsKey(s))
        {
            float lastPlayed = soundTimerDictionary[s];
            if (lastPlayed + s.delay <= Time.time)
            {
                soundTimerDictionary[s] = Time.time;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    // Start is called before the first frame update
    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("no sound");
            return;
        }
        if (CanPlaySound(s))
        {
            GameObject obj = new GameObject("Sound");
            AudioSource audio = obj.AddComponent<AudioSource>();
            audio.clip = s.clip;
            audio.volume = s.volume;
            audio.pitch = s.pitch;
            audio.Play();
            timer = 0.5f;
            //s.source.Play();
            UnityEngine.Object.Destroy(obj, audio.clip.length);
        }
        
    }
}
