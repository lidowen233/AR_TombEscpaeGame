using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CatDetect : MonoBehaviour
{
    public GameManager _gameManager;
    private bool getScore = false;
    public int catPhase = 0;
    public string targetTag; // detect tag
    public AudioSource catAudio;
    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag(targetTag))
        {
            Debug.Log("Model with tag " + targetTag + " has entered the circle!");
            _gameManager.Ready2Rotate(catPhase);
            catAudio.Play();

            if(!getScore)
            {   
                getScore = true;
                _gameManager.AddScore();
            }

        }
    }

    void OnTriggerExit(Collider other)
    {
       
        if (other.gameObject.CompareTag(targetTag))
        {
            Debug.Log("Model with tag " + targetTag + " has left the circle.");
            
        }
    }
}
