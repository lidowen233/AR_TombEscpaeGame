using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
public class GameManager : MonoBehaviour
{
    private int score = 0;  // current score
    public int targetScore = 4;  // target score
    public Text m_MyText;
    //public GameObject anchors = [];
    public AudioSource lavaAudio;
    public GameObject[] phaseObjects;

    public GameObject[] boundaryObjects;
    public Text noteText;
    public Text guideText;
    public GameObject guideImg;
    public GameObject nextImg;

    public GameObject guideObject;

    public GameObject catRemind;

    public GameObject alignObject;
    public GameObject catGroups;
    public int MovedCat = 0;
    private Vector3 previousZoomPosition;
    private Vector3[] previousObjectsPosition;

     [SerializeField]
     GameObject zoom;
     [SerializeField]
     GameObject[] objects;
    [SerializeField]        
    PlaneFinderBehaviour planeFinder;

    [SerializeField]
    private PhasePartManager phaseManager;

    // [SerializeField]
    public enum currentState
    {
        Idle,
        Moving,
        Rotating,
        Finished
    };
    public enum currentPhase
    {
        Begin,
        FinishTorch,
        FinishCat,
        FinishMaze,
        FinishPit,
        FinishGlyph
    };
    public currentPhase _phase = currentPhase.Begin;
    public currentState _state = currentState.Moving;
    // when you finish one task, get one point.

    void Start()
    {
        previousZoomPosition = zoom.transform.position;
        previousObjectsPosition = new Vector3[objects.Length];
        for(int i = 0;i < objects.Length; i++)
        {
            previousObjectsPosition[i] = objects[i].transform.position;
        }
        //previousObjectsPosition = objects.transform.position;
    }
    public void AddScore()
    {
        score++;
    }
    public void realignRoom()
    {
        zoom.transform.position = previousZoomPosition;
        for(int i = 0;i < objects.Length; i++)
        {
           objects[i].transform.position = previousObjectsPosition[i];
         }
        //objects.transform.position = previousObjectsPosition;
    }
    public CatPhase GetCurrCatPhase()
    {
        return phaseManager.catPhase;
    }

    public void UpdateCatPhaseHints()
    {
        
        guideObject.SetActive(true);
        nextImg.SetActive(false);
        Debug.Log("cat phase " + phaseManager.catPhase);
        switch (phaseManager.catPhase)
        {
            case CatPhase.Find1Cat:
                guideText.text = "Ready to The 2nd Riddle:\n" + " Under bridges I often lurk,Causing trouble is my quirk.";
                break;
            case CatPhase.Find2Cat:
                guideText.text = "Ready to The last Riddle:\n" + " I run but never walk, what am I?";
                break;
            case CatPhase.FindAllCats:
                guideText.text = "Put the cats in the following order\n\n";
                guideImg.SetActive(true);
                break;
        }

    }

    public void Update2ndHintsForEachPhase()
    {
        guideObject.SetActive(true);
        switch (_phase)
        {

            case currentPhase.Begin:
                guideText.text = "In the first gate,first you need to find the object and get the riddle to start.";
                break;
            case currentPhase.FinishTorch:
                guideText.text = "First you need to solve the riddles to find the corret poster and collect the cat statues.";
                break;
            case currentPhase.FinishCat:
                guideText.text = "First you need to find the object to help you locate the start point.";
                break;
            case currentPhase.FinishMaze:

                break;
            case currentPhase.FinishPit:

                break;
            default:

                break;
        }
    }
    public void UpdateNextHindFor2nd()
    {
        switch (_phase)
        {

            case currentPhase.Begin:
                guideText.text = "In the first gate,first you need to find the object to get the riddle and start.";
                break;
            case currentPhase.FinishTorch:
                guideText.text = "The first Riddle: Cold and clear, yet trapped in place, melt away without a trace.";
                break;
            case currentPhase.FinishCat:
                guideText.text = "First you need to find the object to help you locate the start point.";
                break;
            case currentPhase.FinishMaze:

                break;
            case currentPhase.FinishPit:

                break;
            default:

                break;
        }
    }
    public string WriteNotes()
    {
        string hintText;
        switch (_phase)
        {
            case currentPhase.Begin:
                hintText = phaseManager.GetTorchPhaseHint();
                break;
            case currentPhase.FinishTorch:
                hintText = phaseManager.GetCatPhaseHint();
                break;
            case currentPhase.FinishCat:
                hintText = phaseManager.GetMazePhaseHint();
                break;
            case currentPhase.FinishMaze:
                hintText = phaseManager.GetLavaPhaseHint();
                break;
            case currentPhase.FinishPit:
                hintText = phaseManager.GetGlyphPhaseHint();
                break;
            default:
                hintText = "Use the ladder to escape!";
                break;
        }
        noteText.text = hintText;
        return hintText;
    }

    public currentState GetCurrentState()
    {
        return _state;
    }
    public currentPhase GetCurrentPhase()
    {
        return _phase;
    }
    public void SetGamePhase(currentPhase phase)
    {
        _phase = phase;
    }
    public void MoveCats()
    {
        MovedCat = MovedCat + 1;
        if (MovedCat == 2)
        {
            phaseManager.UpdateCatPhase(CatPhase.Complete);
            _phase = currentPhase.FinishCat;

            guideObject.SetActive(true);
            guideText.text = "Congratulations! You have finished the cat puzzle!";
            catGroups.SetActive(false);
            catRemind.SetActive(false);
        }
    }
    public void FinishTorchPuzzle()
    {
        _phase = currentPhase.FinishTorch;
        phaseManager.UpdateTorchPhase(TorchPhase.Complete);
    }

    public void Ready2Rotate(int phase)
    {
        if (_state == currentState.Moving)
        {
            m_MyText.text = "Moved Statue Successfully";
            _state = currentState.Rotating;
            if (phase == 0)
            {
                guideObject.SetActive(true);
                guideText.text = "Ready to The 2nd Riddle: Under bridges I often lurk,Causing trouble is my quirk.";
            }
        }

    }

    public void FinishAdjusting(int phase)
    {


        if (phase == 1)
        {
            _state = currentState.Moving;
            // first rotating is over
            guideObject.SetActive(true);
            guideText.text = "Ready to The last Riddle: Black and white in stripes I roam, In grassy plains, I make my home.";
        }
        else
        {
            m_MyText.text = "Finish Cat Moving";
            _state = currentState.Finished;
            //_phase = currentPhase.FinishCat;
            Debug.Log(" next step : finish cat");
            _phase = currentPhase.FinishCat;


        }

    }
    public void ShowNextPhase(int i)
    {
        phaseObjects[i].SetActive(true);
    }

    public void HidePrePhase(int i)
    {
        phaseObjects[i].SetActive(false);
    }
    public void HideBoundary(int i )
    {
        boundaryObjects[i].SetActive(false);
    }
    public void FinishMaze()
    {
        _phase = currentPhase.FinishMaze;
        guideObject.SetActive(true);
        guideText.text = "Congratulations! You have passed the maze and got the golden key.\n" ;
        
        phaseManager.UpdateMazePhase(MazePhase.Complete);
    }
    public void FinishPit()
    {
        _phase = currentPhase.FinishPit;
        lavaAudio.Stop();
        ShowNextPhase(4);
        alignObject.SetActive(false);
        //guideObject.SetActive(true);
        //guideText.text = "Congratulations! You have passed the lava and got the golden key.\n" + "Victory is just within reach!";
        phaseManager.UpdateLavaPhase(LavaPhase.Complete);
        planeFinder.enabled = false;

    }

    public void FinishGlyph()
    {
        _phase = currentPhase.FinishGlyph;
        phaseManager.UpdateGlyphPhase(GlyphPhase.Complete);
    }
    public void PlayLava()
    {
        lavaAudio.Play();
    }

}
