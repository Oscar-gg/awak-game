using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class BossAudioManager : MonoBehaviour
{

    public BossSounds[] sounds;

    public void Awake()
    {
        foreach(BossSounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>(); 
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void PlaySound(string name)
    {
        BossSounds s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.Play();
    }

    
}
