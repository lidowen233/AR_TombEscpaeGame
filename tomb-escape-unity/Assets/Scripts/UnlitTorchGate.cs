using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlitTorchGate : MonoBehaviour
{
    [SerializeField]
    public PhasePartManager phaseManager;

    [SerializeField]
    private GameObject LightObject;

    public bool isTorchLit = false;

    void OnTriggerEnter(Collider other)
    {
        bool doesPlayerHaveLitTorch = (int)phaseManager.torchPhase >= (int)TorchPhase.LightPlayerTorch;

        if (doesPlayerHaveLitTorch && !isTorchLit)
        {
            LightTorch();
        }
        else
        {
            // do nothing for now
        }
    }

    private void LightTorch()
    {
        isTorchLit = true;
        // To Do: play light torch sound
        LightObject.SetActive(true);
        // To Do: Show a cool particle effect
    }
}
