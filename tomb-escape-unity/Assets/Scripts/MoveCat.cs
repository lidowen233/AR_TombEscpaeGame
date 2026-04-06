using UnityEngine;
using UnityEngine.UI;

public class MoveCat : MonoBehaviour
{
    public Camera arCamera; 
    private bool isDragging = false;
    private Vector3 initialPosition;
    public Text m_MyText;
    public GameManager _gameManager;
    public string catTag;

    void Start()
    {
        
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
                    if (hit.transform.CompareTag(catTag))
                    {
                        initialPosition = transform.position;
                        Debug.Log("begin to move");
                        isDragging = true;
                        if (_gameManager.GetCurrentState() == GameManager.currentState.Moving)
                            m_MyText.text = "Move Statue to the Cat Plane";
                        
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {   
                if (_gameManager.GetCurrentState() == GameManager.currentState.Moving)
                {
                    Debug.Log("moving");
                    Vector3 touchPosition = arCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, arCamera.WorldToScreenPoint(initialPosition).z));
                    transform.position = new Vector3(touchPosition.x, transform.position.y, touchPosition.z);
                }
               
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;

                
            }
        }
    }
}



