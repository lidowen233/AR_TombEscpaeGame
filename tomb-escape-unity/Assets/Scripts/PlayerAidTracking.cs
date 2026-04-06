using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using TMPro;

public class PlayerAidTracking : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject playerAids;
    public Camera ARCamera;
    public GameObject ProfessorMessage;

    private Transform playerAidTransform;
    private bool shouldUpdateMessage;
    private Transform CameraTransform;
    private Transform MessageTransform;

    private bool isTimerComplete;

    [SerializeField]
    private TextMeshPro messageUI;

    private float distanceInMeters = 1.4f;
    private float heightInMeters = 1.5f;

    void Start()
    {
        playerAids.SetActive(false);

        // set up state to move the UI message based on object
        shouldUpdateMessage = false;
        playerAidTransform = GetComponent<Transform>();

        CameraTransform = ARCamera.GetComponent<Transform>();
        MessageTransform = ProfessorMessage.GetComponent<Transform>();
    }

    void Update()
    {
        if (shouldUpdateMessage)
        {

            BillboardMessage();

        }
    }

    public void OnPlayerAidFound()
    {
        isTimerComplete = false;

        ShowUI();
    }

    public void OnPlayerAidLost()
    {
        // Trigger a Timer to avoid choppiness, only hide after timer completes
        StartCoroutine(HideAfterTimeDelay());
    }

    private IEnumerator HideAfterTimeDelay()
    {
        isTimerComplete = true;
        yield return new WaitForSeconds(5);
        // this may be false if OnPlayerAidFound is ever invoked during the wait
        if (isTimerComplete)
        {
            HideUI();
        }

        isTimerComplete = true;
        yield return new WaitForSeconds(2);
        // this may be false if OnPlayerAidFound is ever invoked during the wait
        if (isTimerComplete)
        {
            HideUI();
        }
    }

    private void HideUI()
    {
        ProfessorMessage.SetActive(false);
        shouldUpdateMessage = false;
    }

    private void ShowUI()
    {
        string textMessage = gameManager.WriteNotes();
        messageUI.text = textMessage;
        ProfessorMessage.SetActive(true);
        //MessageTransform.position = playerAidTransform.position;
        shouldUpdateMessage = true;

        PlaceMessageInFrontOfPlayerAid();
    }

    private void PlaceMessageInFrontOfPlayerAid()
    {
        // Set the Message location to the player aid (smoothly)
        Vector3 newPosition = CameraTransform.position + CameraTransform.forward * distanceInMeters;//Vector3.Lerp(MessageTransform.position, playerAidTransform.position, FollowSpeed * Time.deltaTime);
        MessageTransform.position = new Vector3(newPosition.x, heightInMeters, newPosition.z);

        // Billboarding: make message face camera
        BillboardMessage();
    }

    private void BillboardMessage()
    {
        Vector3 cameraPositionCopy = CameraTransform.position;
        cameraPositionCopy.y = heightInMeters; // Lock the Y-axis to keep the object upright
        MessageTransform.LookAt(cameraPositionCopy);
        MessageTransform.Rotate(0, 180, 0);
    }
}
