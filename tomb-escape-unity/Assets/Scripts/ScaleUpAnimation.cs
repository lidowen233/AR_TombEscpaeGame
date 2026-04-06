using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUpAnimation : MonoBehaviour
{
    private RectTransform uiElement; // The RectTransform of the UI element to scale
    public float targetScale = 2.5f; // Target scale (1.5 means 150% of the original size)
    public float scalingDuration = 0.5f; // Duration of the scaling animation (in seconds)

    private Vector3 originalScale; // Original scale of the UI element
    private bool isScalingUp = false; // Flag to start scaling
    private float elapsedTime = 0f; // Tracks the animation progress

    void Start()
    {
        uiElement = GetComponent<RectTransform>();
        // Store the original scale of the UI element
        originalScale = uiElement.localScale;
    }

    void Update()
    {
        if (isScalingUp)
        {
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate the percentage of the scaling animation
            float t = Mathf.Clamp01(elapsedTime / scalingDuration);

            // Interpolate between original scale and target scale
            uiElement.localScale = Vector3.Lerp(originalScale, originalScale * targetScale, t);

            // Stop the animation once it's done
            if (t >= 1f)
            {
                isScalingUp = false; // Reset scaling flag
                elapsedTime = 0f;    // Reset the timer
            }
        }
    }

    // Call this method to start the scale-up animation
    public void StartScalingUp()
    {
        isScalingUp = true;
        elapsedTime = 0f; // Reset elapsed time before starting
    }

    // Optionally, a method to reset the scale back to the original
    public void ResetScale()
    {
        uiElement.localScale = originalScale;
        isScalingUp = false;
    }
}
