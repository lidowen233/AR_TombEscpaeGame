using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GamePhaseDetector : MonoBehaviour
{

    public int gamePhase;

    // must add phase cubes to define "bounding area"

    [SerializeField]
    private GameObject phase1;

    [SerializeField]
    private GameObject phase2;

    [SerializeField]
    private GameObject phase3;

    [SerializeField]
    private TextMeshProUGUI PhaseLocationUI;

    // other private vars
    private Bounds phase1Bounds;
    private Bounds phase2Bounds;
    private Bounds phase3Bounds;

    private Transform camTransform;


    // Start is called before the first frame update
    void Start()
    {
        // When game starts, assume they are standing in phase 1
        gamePhase = 1;


        // Get boundary of each phase (the game object is a cube)
        phase1Bounds = phase1.GetComponent<Renderer>().bounds;
        phase2Bounds = phase2.GetComponent<Renderer>().bounds;
        phase3Bounds = phase3.GetComponent<Renderer>().bounds;

        // Access camera transform (to grab location later on)
        camTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentLocation = camTransform.position;
        int newPhase = detectCurrentPhase(currentLocation);
        if (gamePhase != newPhase)
        {
            // Debug.Log("Phase Changed!");
            // Can trigger an event to call out that the phase changed
            gamePhase = newPhase;
        }

        // Show in phone UI for testing and debugging
        updatePhaseText(gamePhase, currentLocation);
    }

    // Higher phase takes priority if currentLocation is inside more than one phase
    private int detectCurrentPhase(Vector3 currentLocation)
    {
        if (phase3Bounds.Contains(currentLocation))
        {
            return 3;
        }
        else if (phase2Bounds.Contains(currentLocation))
        {
            return 2;
        }
        else if (phase1Bounds.Contains(currentLocation))
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    private void updatePhaseText(int phase, Vector3 currentLocation)
    {
        PhaseLocationUI.text = "Phase: " + phase + " CurrentLoc: " + currentLocation;
    }
}
