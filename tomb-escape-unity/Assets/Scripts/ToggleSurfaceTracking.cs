using UnityEngine;
using Vuforia;

public class ToggleSurfaceTracking : MonoBehaviour
{
    public PlaneFinderBehaviour planeFinderBehaviour;  
    //public GameObject toggleButton;  

    private bool isTrackingEnabled = true;  

    void Start()
    {
        
        //toggleButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(ToggleSurfaceTrackingState);
    }

    
    public void ToggleSurfaceTrackingState()
    {
        isTrackingEnabled = !isTrackingEnabled;  
        planeFinderBehaviour.enabled = isTrackingEnabled;

        if (isTrackingEnabled)
        {
            Debug.Log("Enable Surface Tracking");
        }
        else
        {
            Debug.Log("Disable Surface Tracking");
        }
    }
    public void DisableToggleSurfaceTracking()
    {
        planeFinderBehaviour.enabled = false;
    }
}

