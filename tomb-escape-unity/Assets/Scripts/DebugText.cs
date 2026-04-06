using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DebugText : MonoBehaviour
{

    [SerializeField]
    public TextMeshProUGUI lockPhaseText;

    [SerializeField]
    public TextMeshProUGUI resizeText;

    [SerializeField]
    public TextMeshProUGUI triggeredText;

    [SerializeField]
    public Lock3Controller lockController;

    [SerializeField]
    public GameObject lockObj;

    private ResizeGameObject resizer;

    // Start is called before the first frame update
    void Start()
    {
        resizer = lockObj.GetComponent<ResizeGameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        lockPhaseText.text = "Lock Phase: " + lockController.currentPhase;
        triggeredText.text = "isEndPlacementTriggered: " + lockController.endPlacementTriggered;
        resizeText.text = "Resizer: " + "\n timePassed: " + RoundFloat(resizer.timePassed) + "\n isScalingUp: " + resizer.isScalingUp;
    }

    private float RoundFloat(float input)
    {
        return Mathf.Round(input * 100.0f) * 0.1f;
    }
}
