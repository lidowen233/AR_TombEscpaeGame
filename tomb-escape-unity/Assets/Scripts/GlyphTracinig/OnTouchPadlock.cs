using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchPadlock : MonoBehaviour
{
    [SerializeField]
    private GameObject midAirPos;

    [SerializeField]
    private HandleTextContent textContent;

    [SerializeField]
    private PhasePartManager phaseManager;

    private bool isLockActivated;

    [SerializeField]
    private AudioSource touchLockSound;

    // Start is called before the first frame update
    void Start()
    {
        isLockActivated = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (!isLockActivated)
        {
            phaseManager.UpdateGlyphPhase(GlyphPhase.FindLock);
            // show the floating lock
            midAirPos.SetActive(true);

            // show the first instruction
            textContent.SetTextContent(0);
            textContent.ShowText();

            // play sound
            touchLockSound.Play();

            // set flag 
            isLockActivated = true;
        }
    }
}
