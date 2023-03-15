using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioSource bgmAudioSource;
    public float volume;

    void Start()
    {
        bgmAudioSource.Play(); // start playing the background music
    }

    void StopBackgroundMusic()
    {
        bgmAudioSource.Stop(); // stop playing the background music
    }

    void Update()
    {
        bgmAudioSource.volume = volume; // set the volume of the audio source
    }

}
