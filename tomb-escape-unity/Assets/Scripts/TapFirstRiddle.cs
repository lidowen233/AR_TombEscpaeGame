using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapFirstRiddle : MonoBehaviour
{

    [SerializeField]
    private Camera arCamera;

    [SerializeField]
    private GameObject riddleButton;

    [SerializeField]
    private GameObject gamePlayUI;

    [SerializeField]
    private GameObject riddleScroll;

    [SerializeField]
    private PhasePartManager phaseManager;

    public GameObject remindUI;
    public Text remindText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool isMouseDown = Input.GetMouseButtonDown(0);
        bool isPhoneTapped = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;

        if (isMouseDown && isTouchingRiddle(Input.mousePosition))
        {
            show2DRiddle();
            phaseManager.UpdateTorchPhase(TorchPhase.FindTorchAndRiddle);
        }
        else if (isPhoneTapped && isTouchingRiddle(Input.GetTouch(0).position))
        {
            show2DRiddle();

            phaseManager.UpdateTorchPhase(TorchPhase.FindTorchAndRiddle);
        }
        else
        {
            //Debug.Log("Did not touch the riddle");
        }
    }

    private bool isTouchingRiddle(Vector3 touchPosition)
    {
        Ray ray = arCamera.ScreenPointToRay(touchPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("First_Riddle"))
            {
                Debug.Log("Touched riddle");
                return true;
            }
        }
        return false;
    }

    private void show2DRiddle()
    {
        Debug.Log("Showing Riddle");
        hide3DRiddle();
        remindUI.SetActive(true);
        remindText.text = "You have have found the object, try to solve the riddle.";
        riddleButton.SetActive(true);
        gamePlayUI.SetActive(false);
        riddleScroll.SetActive(true);
    }

    private void hide3DRiddle()
    {
        GetComponent<Renderer>().enabled = false;
    }
}
