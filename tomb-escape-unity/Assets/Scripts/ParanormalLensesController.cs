using UnityEngine;
using UnityEngine.UI;
using Vuforia;  // Make sure to include Vuforia namespace

public class ParanormalLensesController : MonoBehaviour
{
    public Button toggleButton;        // Button to toggle image tracking
    

    private bool isTrackingEnabled = true;   // To track whether image tracking is enabled

    void Start()
    {
        // Add listener for button click
        toggleButton.onClick.AddListener(ToggleImageTracking);

        // Set initial status indicator
        //UpdateStatusIndicator();
    }

    // Method to toggle image tracking on or off
    public void ToggleImageTracking()
    {
        isTrackingEnabled = !isTrackingEnabled;

        // Find all ImageTargets in the scene and enable/disable their tracking
        foreach (var imageTarget in FindObjectsOfType<ImageTargetBehaviour>())
        {
            imageTarget.enabled = isTrackingEnabled;
        }
        // Log for debugging
        Debug.Log("Image Tracking " + (isTrackingEnabled ? "Enabled" : "Disabled"));
    }

    
}


