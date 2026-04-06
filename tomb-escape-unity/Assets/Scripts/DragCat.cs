using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DragCat : MonoBehaviour
{
    public Camera arCamera;
    public string draggableTag = "Draggable"; 
    public string surfaceTag = "Surface"; 

    public Text movingRemindText;
    public GameManager _gameManager;

    private bool isDragging = false;
    private Transform selectedObject = null;

    public AudioSource placeAudio;
    private Vector2 initialTouchPosition; 
    private float initialRotationAngle; 
    private Collider selectedCollider;
    void Update()
    {
        
        bool isMouseDown = Input.GetMouseButtonDown(0);
        bool isMouseUp = Input.GetMouseButtonUp(0);
        bool isMouseRightDown = Input.GetMouseButton(1);
        bool isPhoneTapped = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        bool isPhoneReleased = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;

        
        if (isMouseDown || isPhoneTapped)
        {
            Ray ray = isMouseDown 
                ? arCamera.ScreenPointToRay(Input.mousePosition) 
                : arCamera.ScreenPointToRay(Input.GetTouch(0).position);
            
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.CompareTag(draggableTag))
                {
                    Debug.Log("Started dragging!");
                    isDragging = true;
                    selectedObject = hit.transform; 
                    selectedCollider = selectedObject.GetComponent<Collider>();
                    selectedCollider.enabled = false;
                }
            }
        }

        // drag model
        if (isDragging && selectedObject != null && _gameManager.GetCurrCatPhase() == CatPhase.FindAllCats)
        {
            Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                
                Vector3 hitPoint = hit.point;
                selectedObject.position = new Vector3(hitPoint.x, selectedObject.position.y, hitPoint.z); // 水平移动
            }
        }

        // release model
        if ((isMouseUp || isPhoneReleased) && isDragging && _gameManager.GetCurrCatPhase() == CatPhase.FindAllCats)
        {
            isDragging = false;
            //if (selectedCollider != null) selectedCollider.enabled = true;

            Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //movingRemindText.SetActive(true);
                Debug.Log("HIT TAG" + hit.transform.tag);
                if (hit.transform.CompareTag(surfaceTag))
                {
                    // add the ui to remind
                    
                    movingRemindText.text = "Placed on the right surface!";
                    Debug.Log("Placed on the surface!");
                    placeAudio.Play();
                    selectedObject.position = hit.point; 
                    _gameManager.MoveCats();

                }
                else
                {
                    if (selectedCollider != null) 
                        selectedCollider.enabled = true;
                    movingRemindText.text = "Released, not on a valid surface.";
                    Debug.Log("Released, not on a valid surface.");
                }
            }

            selectedObject = null; 
        }

       /*
        if (isMouseRightDown && selectedObject != null)
        {
            float rotationSpeed = 100f;
            float rotationInput = Input.GetAxis("Mouse X");
            selectedObject.Rotate(Vector3.up, rotationInput * rotationSpeed * Time.deltaTime);
        }

       
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                initialTouchPosition = touch0.position - touch1.position;
                initialRotationAngle = selectedObject.eulerAngles.y;
            }
            else if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                Vector2 currentTouchPosition = touch0.position - touch1.position;
                float angleDelta = Vector2.SignedAngle(initialTouchPosition, currentTouchPosition);
                selectedObject.rotation = Quaternion.Euler(0, initialRotationAngle + angleDelta, 0);
            }
        }*/
    }
}


