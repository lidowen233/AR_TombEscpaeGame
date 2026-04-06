using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class AlignMannually : MonoBehaviour
{
    public Camera arCamera; 
    public GameObject room;
    private bool Aligned = false;
    private ObserverBehaviour imageTargetObserver;
    private Vector3 previousPosition;
    public GameObject[] roomobjects;
    Vector3 targetPosition;
    private Vector3 initialOffset;
    private Quaternion initialRotation;
    private Vector3[] initialOffsetObjects;
    private Quaternion[] initialRotations;

    public GameObject textRemind;
    //public Text m_MyText;
    
    // Start is called before the first frame update
    void Start()
    {
        initialOffsetObjects = new Vector3[roomobjects.Length];
        initialRotations = new Quaternion[roomobjects.Length];
        
        initialOffset = room.transform.position - arCamera.transform.position;
        initialRotation = room.transform.localRotation;

        //Debug.Log( "1st Room Position: " + room.transform.position );
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
            //UpdateRoomPositionAndRotation();

        }
       

    }
    public void AlignRoom()
    {
        textRemind.SetActive(true);
        StartCoroutine(WaitBeforeRealign());
    }
    IEnumerator WaitBeforeRealign()
    {   
        yield return new WaitForSeconds(1);
        UpdateRoomPositionAndRotation();
        //yield return new WaitForSeconds(2);
        textRemind.SetActive(false);
    }
    void UpdateRoomPositionAndRotation()
    {
        Vector3 currentPosition = arCamera.transform.position + initialOffset;
        room.transform.position = new Vector3(currentPosition.x,room.transform.position.y,currentPosition.z);
        room.transform.rotation = initialRotation;
        
        for(int i = 0;i < roomobjects.Length; i++)
        {
            currentPosition = initialOffsetObjects[i]  +  arCamera.transform.position ;
            roomobjects[i].transform.position = new Vector3(currentPosition.x,room.transform.position.y,currentPosition.z);
            roomobjects[i].transform.rotation = initialRotations[i];
        }
       
       
        //.text = "Room Position: " + room.transform.position  ;
        Debug.Log( "Room Position: " + room.transform.position );
        //Debug.Log( "Room Object Position: " +  roomobjects.transform.position );
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
