using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class RotateCat : MonoBehaviour
{
   private ObserverBehaviour imageTargetObserver;
   
    private bool isTracking = false; 
    public Camera arCamera;
    public GameObject targetGameObject; 
    private Quaternion originalRelativeRotation;
    private bool isControling = false;
    public GameManager _gameManager;
    public Text m_MyText;

    void Start()
    {
        imageTargetObserver = GetComponent<ObserverBehaviour>();
        if (imageTargetObserver)
        {
           imageTargetObserver.OnTargetStatusChanged += OnTargetStatusChanged;
        }
        
            
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.TRACKED )
        {
            if (targetStatus.StatusInfo == StatusInfo.NORMAL)
            {
                Debug.Log("Target Name: " + behaviour.TargetName);
                
                originalRelativeRotation = CalculateRelativeRotation(imageTargetObserver);
                isTracking = true;
                
            }
        }
        else
        {
            if (targetStatus.StatusInfo != StatusInfo.UNKNOWN)
            {
                
                // not tracked
                isTracking = false;

            }
        }

    }

    private void Update()
    {
        if (isTracking)
        {
            Debug.Log("it's tracking");
            // Calculate relative rotation
            var relativeRotation = CalculateRelativeRotation(imageTargetObserver);
            var relativeAngleChange = relativeRotation.eulerAngles - originalRelativeRotation.eulerAngles;
            
            // Make angle as -180 to 180
            relativeAngleChange = new Vector3(
                AdjustAngle(relativeAngleChange.x),
                AdjustAngle(relativeAngleChange.y),
                AdjustAngle(relativeAngleChange.z)
            );
            
            Debug.Log("Relative Rotation: " + relativeAngleChange);

            // Use only y for volume control
            var rotationDelta = relativeAngleChange.y;

            // Ensure volume change direction is fixed
            //float angleChange = rotationDelta * volumeMultiplier * Time.deltaTime;
            //float newVolume = Mathf.Clamp(audioSource.volume + volumeChange, 0f, 1f);
            /// audioSource.volume = newVolume;
            targetGameObject.transform.Rotate(0.0f, rotationDelta , 0.0f, Space.Self);

            if(rotationDelta != 0 && !isControling)
            {
                isControling = true;
                //_gameManager.AddScore();
            }
            // Update original rotation
            originalRelativeRotation = relativeRotation;


        }
    }

    

    public Quaternion CalculateRelativeRotation(ObserverBehaviour behaviour)
    {
        // image target rotation
        Quaternion targetRotation = behaviour.transform.rotation;
        // ar camera rotation
        Quaternion cameraRotation = arCamera.transform.rotation;
        // relative rotation
        Quaternion relativeRotation = Quaternion.Inverse(cameraRotation) * targetRotation;
        return relativeRotation;
    }

    private float AdjustAngle(float angle)
    {
        // make angle as -180 - 180
        if (angle > 180f)
        {
            angle -= 360f;
        }
        return angle;
    }
}
