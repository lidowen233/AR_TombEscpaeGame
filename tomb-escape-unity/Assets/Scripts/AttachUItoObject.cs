using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class AttachUItoObject : MonoBehaviour
{
    public GameObject smallObject; 
    public GameObject uiCanvas;  
    public Vector3 offset = new Vector3(0, 0.1f, 0); 
  
    private ObserverBehaviour targetObserver;

     void Start()
    {
        targetObserver = GetComponent<ObserverBehaviour>();
        if (targetObserver)
        {
           targetObserver.OnTargetStatusChanged += OnTargetStatusChanged;
        }
        
        if (uiCanvas != null)
        {
            //uiCanvas.SetActive(false);
        }
    }
    void Update()
    {
        if (smallObject.activeInHierarchy)
        {
            
            uiCanvas.transform.position = smallObject.transform.position + offset;
            uiCanvas.transform.LookAt(Camera.main.transform);
            uiCanvas.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }
    }
    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.TRACKED )
        
        {
            Debug.Log("...............");
            if (uiCanvas != null)
            {
                uiCanvas.SetActive(true);
            }
        }
       
    }
}
