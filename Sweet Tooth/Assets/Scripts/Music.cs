using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource musicAudioSource;

    void Start()
    {
        musicAudioSource.Play(); // start playing the background music
    }

    void StopBackgroundMusic()
    {
        musicAudioSource.Stop(); // stop playing the background music
    }

    void Update()
    {
        musicAudioSource.volume = GameDataManager.Data.musicVolume; // set the volume of the audio source
    }

}
