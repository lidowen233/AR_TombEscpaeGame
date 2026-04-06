using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueOnStart : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingScreen;

    [SerializeField]
    private AudioSource crashMusic;
    [SerializeField]
    private AudioSource textBeep;
    [SerializeField]
    private GameObject NewMessage;



    void Start()
    {
        StartCoroutine(WaitForMusicToStop());
    }

    private IEnumerator WaitForMusicToStop()
    {
        yield return new WaitForSeconds(1f);
        loadingScreen.SetActive(false);

        yield return new WaitForSeconds(3f);

        crashMusic.Play();

        // Wait for music to stop
        yield return new WaitForSeconds(5.5f);

        // play text sound
        textBeep.Play();

        // show new message
        NewMessage.SetActive(true);
    }
}
