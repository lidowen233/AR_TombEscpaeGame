using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audio;

    public void PlaySentenceAudio(Message message)
    {
        if (message.sentenceAudio != null)
        {
            audio.clip = message.sentenceAudio;
            audio.Play();
        }
    }
}
