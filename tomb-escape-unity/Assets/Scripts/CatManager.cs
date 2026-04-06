using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentCatIndex = 0;
    public GameObject collectWindow;
    public GameObject[] catModels;
    public Text catNumText;
    public Text collectText;

    public AudioSource collectingAudio;

    public GameObject collectBtn;

    [SerializeField]
    private PhasePartManager phaseManager;
    
    public GameObject catgroupstrigger;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CollectCat(int catIndex)
    {
        Debug.Log("clicked cat" +  catIndex +" : "+ currentCatIndex);
        if(catIndex == currentCatIndex) // 0.1.2
        {
            collectWindow.SetActive(true);
            collectText.text = "Are you sure to collect this statue?";
            collectBtn.SetActive(true);
        }
    }
    public void AddCat()
    {
        currentCatIndex = currentCatIndex + 1;
        catNumText.text =  $"{currentCatIndex}/3";

        // play the music

        collectingAudio.Play();
        // disappear the gameobject

        catModels[currentCatIndex - 1].SetActive(false);

        if(currentCatIndex == 3)
        {
            collectText.text = "You already collected all the cats.";
            phaseManager.UpdateCatPhase(CatPhase.FindAllCats);
            catgroupstrigger.SetActive(false);
            collectBtn.SetActive(false);
        }
        else
        {
            collectText.text = $"You already collected {currentCatIndex} cats.";
            collectBtn.SetActive(false);
            if(currentCatIndex == 1)
            {

                phaseManager.UpdateCatPhase(CatPhase.Find1Cat);
            }
            else if(currentCatIndex == 2)
            {
                 phaseManager.UpdateCatPhase(CatPhase.Find2Cat);
            }
             Debug.Log("///cat phase " + phaseManager.catPhase + " " + currentCatIndex);
        }
    }
}
