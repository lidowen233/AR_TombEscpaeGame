using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private Transform thisTransform;
    private Renderer thisRend;

    [SerializeField]
    private GameObject treasure;

    [SerializeField]
    private AudioSource popSound;

    void Start()
    {
        thisTransform = GetComponent<Transform>();
        thisRend = GetComponent<Renderer>();
        GetComponent<DetectClick>().enabled = false;
        thisRend.enabled = false;
    }

    public void EnableLadder()
    {
        thisRend.enabled = true;
        GetComponent<DetectClick>().enabled = true;
        Transform treasureTransform = treasure.GetComponent<Transform>();
        thisTransform.position = treasureTransform.position;
        thisTransform.LookAt(treasureTransform);

        //thisTransform.Rotate(0, 180, 0);

        popSound.Play();
    }
}
