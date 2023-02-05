using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public String name;
    public AudioClip clip;

    [Range(0,1)]
    public float volume;

    [HideInInspector]
    public AudioSource source;
}
