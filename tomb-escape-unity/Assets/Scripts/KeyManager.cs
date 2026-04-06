using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource touchLockSound;

    [SerializeField]
    private PhasePartManager phaseManager;

    [SerializeField]
    private GameObject midAirPos;

    [SerializeField]
    private DialogueTrigger dt;

    [SerializeField]
    private DialogueTrigger dt2;

    public bool foundGoldenKey;

    private Renderer rend;

    void Start()
    {
        foundGoldenKey = false;
        rend = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TryToUnlock()
    {
        if (foundGoldenKey)
        {
            showGlyphTracer();

        }
        else
        {
            dt.TriggerDialogue();
        }
    }

    private void showGlyphTracer()
    {
        Debug.Log("found key and show");
        // hide this lock
        rend.enabled = false;

        // update phase manager
        phaseManager.UpdateGlyphPhase(GlyphPhase.FindLock);

        // show the floating lock
        midAirPos.SetActive(true);

        // show dialogue 2
        dt2.TriggerDialogue();

        // play sound
        touchLockSound.Play();
    }


    public void setGoldenKeyFound()
    {
        Debug.Log("found the keyss");
        foundGoldenKey = true;
    }
}
