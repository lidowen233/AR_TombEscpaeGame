using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleStartPointInMaze : MonoBehaviour
{
    public GameObject startObject;
    public Camera arCamera; 

    public AudioClip startaAudioClip;
    public AudioSource gridAudio;
    
    public Text guideText;
    public GameObject guideObject;
    
    [SerializeField]
    private PhasePartManager phaseManager;


    // Start is called before the first frame update
    void Start()
    {
        
    }    
    void Update()
    {
        bool isMouseDown = Input.GetMouseButtonDown(0);
        bool isPhoneTapped = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;

        if(isMouseDown )
        {
            Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        LocateStartPoint();
                    }
                }
        }
        else if(isPhoneTapped)
        {
            Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        LocateStartPoint();
                    }
                }
        }

        
    }
    
    public void LocateStartPoint()
    {
        startObject.SetActive(true);
       
        //ShowMessage("You already found the start point!");
        PlayStartMusic();
        guideObject.SetActive(true);
        StartCoroutine(OpenMaze());
       phaseManager.UpdateMazePhase(MazePhase.FindMazeStart);

    }
    IEnumerator OpenMaze()
    {
        guideText.text = "Congratulations! You have found the start point of maze. Next you need to find the route of maze.";
        yield return new WaitForSeconds(3);
        guideText.text = "Finish this riddle to find the maze route: Black and white in stripes I roam. Grassy plains, my home.";
        
    }
    public void PlayStartMusic()
    {
        gridAudio.Play(); 
    }

}
