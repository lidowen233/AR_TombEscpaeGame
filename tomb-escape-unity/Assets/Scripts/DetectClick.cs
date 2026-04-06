using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DetectClick : MonoBehaviour
{
    public Camera arCamera;

    public CustomEventType customEvent;

    private GameObject thisGameObject;

    private string hitGameObjectName;
    private string thisGameObjectName;

    // Start is called before the first frame update
    void Start()
    {
        thisGameObject = gameObject;
        thisGameObjectName = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {

        bool isMouseDown = Input.GetMouseButtonDown(0);
        bool isPhoneTapped = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;

        if (isMouseDown && isTouchingThis(Input.mousePosition))
        {
            TriggerCustomEvent();
        }
        else if (isPhoneTapped && isTouchingThis(Input.GetTouch(0).position))
        {
            TriggerCustomEvent();
        }
    }

    private void TriggerCustomEvent()
    {

        if (customEvent != null)
        {
            customEvent.Invoke();
        }
    }

    private bool isTouchingThis(Vector3 touchPosition)
    {
        Ray ray = arCamera.ScreenPointToRay(touchPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            hitGameObjectName = hit.transform.gameObject.name;
            bool hitThisObject = hitGameObjectName == thisGameObjectName;
            if (hitThisObject)
            {
                return true;
            }
        }
        return false;
    }
}
