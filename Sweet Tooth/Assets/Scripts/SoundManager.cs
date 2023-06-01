using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioSource> audioSources;
    public float audioSourceAmount; // number of audio sources, increase if too many sounds

    void Start()
    {
        // populate the audio source list with new audio sources
        for (int i = 0; i < audioSourceAmount; i++)
        {
            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            audioSources.Add(newAudioSource);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        // find the first available audio source
        AudioSource availableAudioSource = audioSources.Find(a => !a.isPlaying);

        // if all audio sources are currently playing, do nothing
        if (availableAudioSource == null)
        {
            return;
        }

        // play the sound using the available audio source
        availableAudioSource.clip = clip;
        availableAudioSource.volume = GameDataManager.Data.soundsVolume;
        availableAudioSource.Play();
    }

    void Update()
    {
        // update the volume of all the audio sources every frame based on the current value of the volume variable
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = GameDataManager.Data.soundsVolume;
        }
    }
}
