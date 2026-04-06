using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeGameObject : MonoBehaviour
{

    private Transform transform;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    // modify in unity editor for easier debugging
    public float scalePeriod; // interval of time for scale up and scale down
    public float MaxLockSize;
    public float MinLockSize;


    // used for increase And Decrease Scale logic 
    public bool isScalingUp;
    public float timePassed;

    // increases scale of game object to max size, then decreases to min size, repeat
    public void increaseAndDecreaseScale()
    {
        Vector3 minScale = new Vector3(MinLockSize, MinLockSize, MinLockSize);
        Vector3 maxScale = new Vector3(MaxLockSize, MaxLockSize, MaxLockSize);

        timePassed += Time.deltaTime;

        if (isScalingUp)
        { // scale up
            transform.localScale = Vector3.Lerp(minScale, maxScale, timePassed / scalePeriod);
        }
        else // scale down
        {
            transform.localScale = Vector3.Lerp(maxScale, minScale, timePassed / scalePeriod);
        }

        if (timePassed > scalePeriod)
        {
            isScalingUp = !isScalingUp;
            timePassed = 0.0f;
        }
    }
}
