using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManagerSimple : MonoBehaviour
{
    
    public Text noteText;

    [SerializeField]
    private PhasePartManager phaseManager;
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
        FinishPit
    };
    public currentPhase _phase = currentPhase.Begin;
    public currentState _state = currentState.Moving;
    // when you finish one task, get one point.

    void Start()
    {

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
    
}
