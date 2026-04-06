using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TriggerBoundary : MonoBehaviour
{
    public GameManager _gameManager;

    public AudioSource movementAudio;
    public AudioSource successSound;
    public AudioSource alertAudio;
    public int phase = 0;
    public GameObject remindObject;
    
    public GameObject Gate;
    public Text remindText;
    public GameObject guideObject;
    public GameObject guideArrow;
    public Text guideText;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MainCamera")
        {
            if(!checkPhase() )
            {
                remindObject.SetActive(true);
                alertAudio.Play();
            }
            else
            { 
                if(!(phase == 0 && _gameManager.GetCurrentPhase() == GameManager.currentPhase.FinishTorch))
                {    
                    //remindObject.SetActive(true);
                    Gate.SetActive(false);
                    remindText.text = "";
                }
                showGuide();
                StartCoroutine(OpenGate());
                
            }
        }
       
    }
    bool checkPhase(){
        if(( phase == 0 && _gameManager.GetCurrentPhase() == GameManager.currentPhase.FinishTorch) || 
        (phase == 1 && _gameManager.GetCurrentPhase() == GameManager.currentPhase.FinishCat) ||
        (phase == 2 && _gameManager.GetCurrentPhase() == GameManager.currentPhase.FinishMaze))
            return true;
        else
            return false;

    }
    void OnTriggerExit(Collider other)
    {
        remindObject.SetActive(false);
        guideObject.SetActive(false);
        alertAudio.Stop();
        StartCoroutine(WaitSeconds());
        if(( phase == 0 && _gameManager.GetCurrentPhase() == GameManager.currentPhase.FinishTorch))
        {   
            _gameManager.HideBoundary(0);
        }
        if(( phase == 1 && _gameManager.GetCurrentPhase() == GameManager.currentPhase.FinishCat))
        {  
            _gameManager.HideBoundary(1); 
        }
        if(phase == 2 &&  _gameManager.GetCurrentPhase() == GameManager.currentPhase.FinishMaze)
        {
             _gameManager.HideBoundary(2);
        }
    }
    public void showGuide()
    {
        if(( phase == 0 && _gameManager.GetCurrentPhase() == GameManager.currentPhase.FinishTorch))
        {   
            guideObject.SetActive(true);
            _gameManager.ShowNextPhase(1);
             //guideObject.GetComponent<TypewriterEffect>().SetText("Welcome to Door 2, here you need to solve a series of cat puzzles to escape! Go and get help!");
            guideText.text = "Welcome to Door 2, here you need to solve a series of cat puzzles to escape! Go and get help!";
            guideArrow.SetActive(true);
            _gameManager.HidePrePhase(0);
        }
        if(( phase == 1 && _gameManager.GetCurrentPhase() == GameManager.currentPhase.FinishCat))
        {   
            guideArrow.SetActive(true);
            guideObject.SetActive(true);
            _gameManager.ShowNextPhase(2);
            _gameManager.HidePrePhase(1);
            guideText.text = "Welcome to Door 3, here you need to go through the ghost maze to get the key!";

            //guideObject.GetComponent<TypewriterEffect>().SetText("Welcome to Door 3, here you need to go through the ghost maze to get the key!");
        }
        if(phase == 2 &&  _gameManager.GetCurrentPhase() == GameManager.currentPhase.FinishMaze)
        {
            guideObject.SetActive(true);
            _gameManager.PlayLava();
            _gameManager.ShowNextPhase(3);
             _gameManager.HidePrePhase(2);
            guideText.text = "Welcome to Door 4, here you need to go cross the lava to get the golden key!";
            //guideObject.GetComponent<TypewriterEffect>().SetText("Welcome to Door 4, here you need to go cross the lava to escape!");
        }
    }
    IEnumerator OpenGate()
    {
        // Play Success Sound
        successSound.Play();
        // wait 2 seconds
        yield return new WaitForSeconds(2);
        
        // Play some rock moving audio
        movementAudio.Play();
        yield return new WaitForSeconds(2); 
        movementAudio.Stop();
        
    }
    IEnumerator WaitSeconds()
    {

        yield return new WaitForSeconds(3);
    }
}
