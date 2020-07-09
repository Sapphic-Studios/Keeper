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
            soundTimerDictionary[s] = -1000f; 
        }
        PlaySound("Drone", false);
        PlaySound("Die", false);
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
                return true;
            else
                return false;
        }
        else
            return false;
    }
    private Sound findSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("no sound");
            return null;
        }return s;
    }
    private void GenerateSound(Sound s)
    {
        GameObject obj = new GameObject("Sound");
        AudioSource audio = obj.AddComponent<AudioSource>();
        audio.clip = s.clip;
        audio.volume = s.volume;
        audio.pitch = s.pitch;
        audio.loop = s.loop;
        audio.Play();
        soundTimerDictionary[s] = Time.time;
        //s.source.Play();
        if(!audio.loop)
            UnityEngine.Object.Destroy(obj, audio.clip.length);
    }
    // Start is called before the first frame update
    public void PlaySound(string name, bool oneShot)
    {
        Sound s = findSound(name);
        if (s == null) return;
        if (oneShot || CanPlaySound(s))
        {
            GenerateSound(s);
        }
    }
    public void PlaySound(string name, Vector3 position)
    {
        Sound s = findSound(name);
        if (s == null) return;
        if (CanPlaySound(s))
        {
            GameObject obj = new GameObject("Sound");
            obj.transform.position = position;
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
