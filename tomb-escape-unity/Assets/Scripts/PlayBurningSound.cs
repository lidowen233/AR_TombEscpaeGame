using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBurningSound : MonoBehaviour
{
    AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
        audioData.Pause();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startBurningMusic()
    {
        if (audioData != null)
        {
            audioData.UnPause();
        }

    }

    public void stopBurningMusic()
    {
        if (audioData != null)
        {
            audioData.Pause();
        }
    }
}
