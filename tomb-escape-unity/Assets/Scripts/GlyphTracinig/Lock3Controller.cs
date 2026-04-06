using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Lock3Controller : MonoBehaviour
{

    [SerializeField]
    private Camera arCamera;

    [SerializeField]
    private Treasure treasure;

    [SerializeField]
    private GameObject[] Lock3Icons;

    [SerializeField]
    private PuzzleTextController puzzleText;

    [SerializeField]
    private FeedbackController userFeedback;

    [SerializeField]
    private AudioSource touchGlyphSound;

    [SerializeField]
    private AudioSource successSound;

    [SerializeField]
    private ResizeGameObject resizer;

    [SerializeField]
    private HandleTextContent textContent;

    public GameObject remindUI;
    public Text remindText;

    [SerializeField]
    private PhasePartManager phaseManager;

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private DialogueTrigger dt;

    private float timePassed;


    public bool endPlacementTriggered; // used for debugging

    public enum lockPhase
    {
        Inactive,
        Placement, // lock is being placed somewhere using the mid-air positioner
        ActivePuzzle,
        Complete
    };

    // Can switch phase in Unity Editor for easy debugging
    public lockPhase currentPhase = lockPhase.Inactive;

    void Start()
    {
        timePassed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPhase == lockPhase.ActivePuzzle)
        {
            HandleUserInput();
        }
        else if (currentPhase == lockPhase.Placement)
        {
            timePassed += Time.deltaTime;

            HandleLockResize();
            // Show Instruction
            //textContent.SetTextContent(1);
            //textContent.ShowText();

            // wait 2 seconds to prevent lock from being placed prematurely
            if (timePassed > 2)
            {
                MaybeEndPlacementPhase();
            }

        }
        else if (currentPhase == lockPhase.Complete)
        {
            ActivateTreasure();
        }
    }

    private void HandleUserInput()
    {
        bool isMouseDown = Input.GetMouseButton(0) || Input.GetMouseButtonDown(0);
        bool isMouseUp = Input.GetMouseButtonUp(0);

        if (isMouseDown)
        {
            handleIconTouch(Input.mousePosition);
        }
        else if (isPhoneTouch())
        {
            handleIconTouch(Input.GetTouch(0).position);
        }
        else if (isMouseUp)
        {
            handleEndSelection();
        }
        else if (isEndPhoneTouch())
        {
            handleEndSelection();
        }
        else
        {
            //Debug.Log("Did not touch anything");
        }
    }

    private bool handleIconTouch(Vector3 touchPosition)
    {
        Ray ray = arCamera.ScreenPointToRay(touchPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Collider touchedCollider = hit.collider;
            if (touchedCollider != null) // if (hit.transform.CompareTag("First_Riddle"))
            {
                GameObject touchedObject = touchedCollider.gameObject;
                Lock3Icon lockIcon = touchedObject.GetComponent<Lock3Icon>();

                if (lockIcon != null)
                {
                    bool isIconSelected = lockIcon.MaybeSelectIcon();

                    if (isIconSelected)
                    {
                        touchGlyphSound.Play();
                        puzzleText.SetPuzzleText(lockIcon.secretLetter);
                    }
                }
                return true;
            }



        }
        return false;
    }

    private bool isPhoneTouch()
    {
        bool isPhoneTapped = Input.touchCount > 0;
        bool isPhoneTouchDown = false;
        if (isPhoneTapped)
        {
            isPhoneTouchDown = Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Moved;
        }
        return isPhoneTapped && isPhoneTouchDown;
    }

    private bool isEndPhoneTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            return touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled;
        }
        return false;
    }

    // This method is called when user lets go of touching/pressing the screen
    private void handleEndSelection()
    {
        resetIcons();
        verifyIconSelection();
    }

    private void resetIcons()
    {

        foreach (GameObject icon in Lock3Icons)
        {
            icon.GetComponent<Lock3Icon>().ResetIcon();
        }
    }

    // Check if user pressed icons in the correct order
    private void verifyIconSelection()
    {
        if (puzzleText.GetPuzzleText().Length == 6)
        {
            bool isSelectionCorrect = puzzleText.CheckPuzzleText();
            if (isSelectionCorrect)
            {
                Debug.Log("CORRECT");
                userFeedback.ShowCorrectSprite();
                StartCoroutine(winPuzzle());
            }
            else
            {
                Debug.Log("FALSE");
                userFeedback.ShowWrongSprite();
                puzzleText.ResetPuzzleText();
            }
        }
        else
        {
            Debug.Log("DID NOT ENTER ALL 6 LETTERS");
            puzzleText.ResetPuzzleText();
        }
    }

    private IEnumerator winPuzzle()
    {
        successSound.Play();


        phaseManager.UpdateGlyphPhase(GlyphPhase.SolveLock);
        gameManager.FinishGlyph();
        // Show Instruction
        //textContent.SetTextContent(2);
        //textContent.ShowText();

        yield return new WaitForSeconds(6);

        currentPhase = lockPhase.Complete;

    }

    // method called when lock is placed
    public void onPlaceLock()
    {
        // bugfix: success icon renders when lock is placed
        userFeedback.HideFeedback();

        // update lock phase so it will start resizing
        currentPhase = lockPhase.Placement;

        // show message
        dt.TriggerDialogue();

        touchGlyphSound.Play();
    }

    // lock will start small and slowly increase size of lock, and repeat
    // add instruction to tell them to release to determine size
    // when user releases mouse or finger tap, the lock size will stop growing
    private void HandleLockResize() // Called in Update loop
    {
        // start resizing lock in update loop
        Debug.Log("Resizing lock");
        resizer.increaseAndDecreaseScale();
    }

    // listen for the event that will end puzzle placement
    // the event that ends puzzle placement: user taps the screen
    private void MaybeEndPlacementPhase()
    {
        bool isMouseDown = Input.GetMouseButton(0) || Input.GetMouseButtonDown(0);
        if (isMouseDown || isPhoneTouch())
        {
            endPlacementTriggered = true;
            // end Placement phase and start Puzzle phase
            currentPhase = lockPhase.ActivePuzzle;
            touchGlyphSound.Play();
            // hide text
            //textContent.HideText();
            phaseManager.UpdateGlyphPhase(GlyphPhase.PlaceLock);
        }
    }

    private void ActivateTreasure()
    {
        treasure.EnableTreasure();
        gameObject.SetActive(false);
    }
}
