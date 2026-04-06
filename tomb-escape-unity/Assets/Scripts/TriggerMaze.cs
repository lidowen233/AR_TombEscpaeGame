using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
public class TriggerMaze : MonoBehaviour
{
    private ObserverBehaviour imageTargetObserver;
    public Text guideText;
    public GameObject guideObject;
    [SerializeField]
    private PhasePartManager phaseManager;
    // Start is called before the first frame update
    void Start()
    {
        imageTargetObserver = GetComponent<ObserverBehaviour>();
        if (imageTargetObserver)
        {
           imageTargetObserver.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.TRACKED )
        {
            if (targetStatus.StatusInfo == StatusInfo.NORMAL)
            {
                guideObject.SetActive(true);
                guideText.text = "Now follow the route to go through the maze!";
                phaseManager.UpdateMazePhase(MazePhase.FindMazeRoute);
            }
        }
        
    }

}
