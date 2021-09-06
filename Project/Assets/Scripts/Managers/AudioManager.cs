using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void TogglePlaying()
    {
        if (source.isPlaying)
            source.Stop();
        else
            source.Play();
    }
    
    public void Play()
    {
        source.Play();
    }
}
