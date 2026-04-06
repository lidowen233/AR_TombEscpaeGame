using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class AllignRoom : MonoBehaviour
{
    public Camera arCamera; 
    public GameObject room;
    private bool Aligned = false;
    private ObserverBehaviour imageTargetObserver;
    private Vector3 previousPosition;
    public GameObject[] roomobjects;
    Vector3 targetPosition;
    private Vector3 initialOffset;
    private Vector3[] initialOffsetObjects;

    private Quaternion initialRotation;
    private Quaternion[] initialRotations;
    
    // Start is called before the first frame update
    void Start()
    {
        initialOffsetObjects = new Vector3[roomobjects.Length];
        initialRotations = new Quaternion[roomobjects.Length];

        initialOffset = room.transform.position - transform.position;
        initialRotation = room.transform.localRotation;

        for(int i = 0;i < roomobjects.Length; i++)
        {
            initialOffsetObjects[i] = roomobjects[i].transform.position - arCamera.transform.position;
            initialRotations[i] = roomobjects[i].transform.localRotation;
        }

        imageTargetObserver = GetComponent<ObserverBehaviour>();
        if (imageTargetObserver)
        {
           imageTargetObserver.OnTargetStatusChanged += OnTargetStatusChanged;
        }
          
    }
     private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.TRACKED)
        {
            //catAudio.Play();
        }
        if (targetStatus.Status == Status.TRACKED  && !Aligned)
        {
            Aligned = true;
            targetPosition = behaviour.transform.position;
            Debug.Log("target : " + targetPosition + " frame " + transform.position + "camera "+ arCamera.transform.position);
            UpdateRoomPositionAndRotation();

        }
       

    }
    void UpdateRoomPositionAndRotation()
    {
        Vector3 currentPosition = targetPosition + initialOffset;
        room.transform.position = new Vector3(currentPosition.x,room.transform.position.y,currentPosition.z);
        room.transform.rotation = initialRotation;
        for(int i = 0;i < roomobjects.Length; i++)
        {
            currentPosition = initialOffsetObjects[i]  +  arCamera.transform.position ;
            roomobjects[i].transform.position = new Vector3(currentPosition.x,room.transform.position.y,currentPosition.z);
            roomobjects[i].transform.rotation = initialRotations[i];
        }
        
        
        
        //m_MyText.text = "Room Position: " + room.transform.position + ", Frame Position: " + targetPosition;
        Debug.Log( " Room Position: " + room.transform.position );

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
