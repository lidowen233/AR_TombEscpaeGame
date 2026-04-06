using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        // audioData.Play(0); // do not play music for now
    }
    public void StartGame()
    {
        Debug.Log("Start Game!");
        audioData.Stop();
        SceneManager.LoadScene("Tomb001 backup");
    }
}
