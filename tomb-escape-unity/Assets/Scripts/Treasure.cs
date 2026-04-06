using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public Camera arCamera;

    private Transform thisTransform;
    private Renderer thisRend;

    public AudioSource popSound;
    public AudioSource successSound;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();
        thisRend = GetComponent<Renderer>();

        thisRend.enabled = false;
    }

    public void EnableTreasure()
    {
        Debug.Log("Show Treasure");
        thisRend.enabled = true;

        Transform player = arCamera.GetComponent<Transform>();
        Transform treasure = thisTransform;

        treasure.position = player.position + player.forward * 2.0f;
        treasure.LookAt(player);

        popSound.Play();
        StartCoroutine(DropTreasure());
    }

    private IEnumerator DropTreasure()
    {
        float targetYValue = 0f;
        float timeDuration = 1.0f;

        // Get the object's initial position
        Vector3 startPosition = thisTransform.position;

        // Calculate the target position
        Vector3 targetPosition = new Vector3(startPosition.x, targetYValue, startPosition.z);

        // Track the elapsed time
        float elapsedTime = 0f;

        // Loop over timeDuration
        while (elapsedTime < timeDuration)
        {
            // Lerp the position based on the elapsed time
            thisTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / timeDuration);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Wait until the next frame
            yield return null;
        }

        // Ensure the object ends up at the exact target position
        thisTransform.position = targetPosition;
        successSound.Play();
    }
}
