using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PitCrossing : MonoBehaviour
{
    private bool isOnWoodenPlatform = false;
    private bool isOnDestination = false;
    public Text m_MyText;
    public GameObject remindObject;
    public GameManager _gameManager;
    public GameObject LavaPit;
    public GameObject goldenKey;
    public DialogueTrigger dialogue;
    public KeyManager keyManager;
    public AudioSource successAudio;
    private float timeInForbiddenArea = 0f;
    private float maxTimeInForbiddenArea = 2f;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "WoodenPlatform":
                isOnWoodenPlatform = true;
                //m_MyText.text = "Jump";
                Debug.Log("Player is on a wooden platform.");
                break;

            case "Destination":
                isOnDestination = true;
                successAudio.Play();
                Debug.Log("Player arrived at the destination.");
                goldenKey.SetActive(false);
                _gameManager.FinishPit();

                

                break;

            case "ForbiddenArea":
                if (!isOnWoodenPlatform)
                {
                    Debug.Log("Player fell into the forbidden area.");
                    GameOver();
                }
                break;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "WoodenPlatform":
                isOnWoodenPlatform = false;
                Debug.Log("Player left the wooden platform.");
                break;

            case "ForbiddenArea":
                Debug.Log("Player left the forbidden area.");
                m_MyText.text = "";
                if (!isOnDestination)
                    Restart();
                remindObject.SetActive(false);
                break;
            case "Destination":
                // trigger game finale
                keyManager.setGoldenKeyFound();
                dialogue.TriggerDialogue();

                // disable this whole lava pit part of the game 
                LavaPit.SetActive(false);
                //if(isOnDestination)
                //    Restart();
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("ForbiddenArea"))
        {
            if (!isOnWoodenPlatform)
            {
                timeInForbiddenArea += Time.deltaTime;
                if (timeInForbiddenArea >= maxTimeInForbiddenArea)
                {
                    Debug.Log("Player stayed too long in the forbidden area.\n Go back and restart.");
                    GameOver();
                    timeInForbiddenArea = 0f;
                }
            }
            else
            {
                timeInForbiddenArea = 0f;
                m_MyText.text = "";
                remindObject.SetActive(false);
                Debug.Log("Player is safe on the wooden platform.");
            }
        }
    }


    void GameOver()
    {
        if (!isOnDestination)
        {
            m_MyText.text = "Player stayed too long in the forbidden area.";
            remindObject.SetActive(true);
            Debug.Log("Game Over. You entered the forbidden area.");
        }
    }
    void Restart()
    {
        PlatformMonobehaviour[] platforms = FindObjectsOfType<PlatformMonobehaviour>();
        foreach (PlatformMonobehaviour platform in platforms)
        {
            Destroy(platform.gameObject);

        }
    }
}
