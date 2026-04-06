using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Trigger1stRemindForCatPuzzle : MonoBehaviour
{
    /// <summary>
    ///  this script is used to connect with the map button to trigger the second remind after the boundary remind.
    /// </summary>
    public GameManager _gameManager;
    public GameObject guideObject;

    public GameObject guideImg;
    public Text guideText;
    public GameObject riddleObject;
    public GameObject glyphObject;
    private bool started = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetTorchRiddle()
    {
        
    }
    public void GetFirstRiddle()
    {
        if(_gameManager.GetCurrentPhase() == GameManager.currentPhase.Begin)
        {
            riddleObject.SetActive(true);
        }
       
        if((_gameManager.GetCurrentPhase() == GameManager.currentPhase.FinishTorch))
        {   
            if(_gameManager.GetCurrCatPhase() == CatPhase.Start)
            {
                guideObject.SetActive(true);
                guideText.text = "The first Riddle: Cold and clear, yet trapped in place, melt away without a trace.";
            }
            else if(_gameManager.GetCurrCatPhase() == CatPhase.FindAllCats)
            {
                guideObject.SetActive(true);
                guideText.text = "Put the cats in the following order\n\n";
                guideImg.SetActive(true);
            }
         
            
        }
        else if(_gameManager.GetCurrentPhase() == GameManager.currentPhase.FinishCat)
        {
            guideObject.SetActive(true);
            guideText.text = "Go to find the start point!";
        }
        else if(_gameManager.GetCurrentPhase() == GameManager.currentPhase.FinishPit)
        {
            glyphObject.SetActive(true);
            //guideText.text = "Go to find the start point!";
        }

     
    }
}
