using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMonobehaviour : MonoBehaviour
{
    public Material newMat;
    public Material originalMat;
    private AudioSource audioSource;
    private bool hasPlayed = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void ActivatePlatform()
    {
        hasPlayed = false;
        Renderer cellRenderer = GetComponent<Renderer>();
        if (cellRenderer != null)
        {
            cellRenderer.material = originalMat;
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // when ar camera enter the grid
        if (other.CompareTag("MainCamera"))  
        {
            Renderer cellRenderer = GetComponent<Renderer>();

            if (cellRenderer != null)
            {
                //cellRenderer.material = newMat;
                cellRenderer.material.color = new Color(70f / 255f, 16f / 255f, 4f / 255f,100/255f);;
            }
            if(!hasPlayed)
            {
                audioSource.Play();
                hasPlayed = true;
            }

        }
    }
    
}
