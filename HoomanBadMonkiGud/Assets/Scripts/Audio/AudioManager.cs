using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] Sounds;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }

        foreach(Sound s in Sounds) 
        { 
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
        }
    }

    public void PlayByName(string name)
    {
        Sound s = Array.Find(Sounds, s => s.name == name);
        if (s == null)
        {
            Debug.LogError("Sound " + name + " not found!");
            return;
        }
        Debug.Log("Playing " + s.name);
        s.source.Play();
    }

    public void PlayByIndex(int index)
    {
        if(index < 0 || index > Sounds.Length)
        {
            Debug.LogError("Index " + index + " out of bounds for Sounds!");
            return;
        }
        Debug.Log("Playing " + Sounds[index].name);
        Sounds[index].source.Play();
    }

}
