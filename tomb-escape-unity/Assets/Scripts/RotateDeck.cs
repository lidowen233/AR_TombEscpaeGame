using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateDeck : MonoBehaviour
{
    public Camera arCamera; 
    private bool isDragging = false;
    public int phase;
    public float rotationSpeed = 0.2f;
    public Text m_MyText;
    public GameManager _gameManager;
    public Vector3 targetAngle;
    public float angleThreshold = 1.0f; 
    public string deckTag;
    public AudioSource catAudio;
    public AudioClip wrongAudioClip;
    public AudioClip rightAudioClip;
    private Quaternion initialRotation;
    void Start()
    {
       initialRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {   
                    if (hit.transform.CompareTag(deckTag))
                    {
                        Debug.Log("begin to rotate " + _gameManager.GetCurrentState() + " " + deckTag + " "+ phase);
                        isDragging = true;

                        if (_gameManager.GetCurrentState() == GameManager.currentState.Rotating)
                            m_MyText.text = "Rotate the Statue deck";
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {   
                if (_gameManager.GetCurrentState() == GameManager.currentState.Rotating)
                {
                    Debug.Log("rotating");
                    float rotationX = touch.deltaPosition.y * rotationSpeed;
                    //float rotationY = -touch.deltaPosition.x * rotationSpeed;
                    transform.Rotate(0,rotationX, 0, Space.Self);
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
            
                if (_gameManager.GetCurrentState() == GameManager.currentState.Rotating && isDragging)
                {
                    isDragging = false;
                    float angleDifference = Quaternion.Angle(transform.rotation, initialRotation);
                    angleDifference = (angleDifference > 90) ? (angleDifference - 90) : (90 - angleDifference);
                    Debug.Log("rotated " + angleDifference);
                    if (angleDifference <= angleThreshold)
                    {
                        catAudio.clip = rightAudioClip; 
                        catAudio.Play();
                        m_MyText.text = "Finishing Rotating";
                        _gameManager.FinishAdjusting(phase);
                    }
                    else
                    {
                         catAudio.clip = wrongAudioClip; 
                         catAudio.Play(); 
                    }
                }
            }
        }
    }
}
