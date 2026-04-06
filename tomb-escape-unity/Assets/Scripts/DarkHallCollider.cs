using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkHallCollider : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private UnlitTorchGate LeftTorch;

    [SerializeField]
    private UnlitTorchGate RightTorch;

    public GameObject DoorGroup;
    public GameObject DarkUI;

    public AudioSource movementAudio;
    public AudioSource successSound;


    private bool shouldMoveGate;
    private Transform doorTransform;

    public float gateMoveSpeed;

    private float gateFinalYValue;

    private MeshRenderer renderer;

    void Start()
    {
        doorTransform = DoorGroup.GetComponent<Transform>();
        renderer = GetComponent<MeshRenderer>();

        gateFinalYValue = 6.0f;
    }

    void Update()
    {
        if (shouldMoveGate)
        {
            MoveGate();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (FinishTorchPhase())
        {
            StartCoroutine(OpenGate());
        }
        else
        {
            DarkAtmosphere();
        }

    }

    void OnTriggerExit(Collider other)
    {
        DarkUI.SetActive(false);
    }

    private bool FinishTorchPhase()
    {
        bool areTorchesLit = LeftTorch.isTorchLit && RightTorch.isTorchLit;
        if (areTorchesLit)
        {
            gameManager.FinishTorchPuzzle();
            return true;
        }
        return false;
    }

    IEnumerator OpenGate()
    {
        Debug.Log("Light the Torches");


        // Play Success Sound
        successSound.Play();
        // wait 2 seconds
        yield return new WaitForSeconds(2);
        // Play some rock moving audio
        movementAudio.Play();
        // animate the gate to slowly move upwards and then turn off these gameobjects
        shouldMoveGate = true;
    }

    public void DarkAtmosphere()
    {
        Debug.Log("It's too dark to see this gate");
        // set UI canvas overlay to dark
        // say some text that it's too dark in here
        DarkUI.SetActive(true);
        // TO DO: play some audio reading "its too dark"
    }

    public void MoveGate()
    {
        Vector3 currentPosition = doorTransform.position;
        Vector3 targetPosition = new Vector3(currentPosition.x, gateFinalYValue, currentPosition.z);

        doorTransform.position = Vector3.MoveTowards(currentPosition, targetPosition, gateMoveSpeed * Time.deltaTime);

        // Stop moving and hide when the target position is reached
        if (Mathf.Approximately(doorTransform.position.y, gateFinalYValue))
        {
            movementAudio.Stop();
            shouldMoveGate = false;

            DoorGroup.SetActive(false);
        }
    }
}
