using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MusicController : MonoBehaviour
{
   
    private AudioSource[] allAudioSources;

    void Start()
    {
        
        allAudioSources = FindObjectsOfType<AudioSource>();

      
    }

    public void UnmuteMusic()
    {
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.mute = false; 
        }
    }
    public void MuteMusic()
    {
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.mute = true; 
        }
    }
}
