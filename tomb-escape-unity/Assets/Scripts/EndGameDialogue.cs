using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameDialogue : MonoBehaviour
{
    [SerializeField]
    private DialogueTrigger dt;

    void Start()
    {
        StartCoroutine(DialogueAfterPause());
    }

    private IEnumerator DialogueAfterPause()
    {
        yield return new WaitForSeconds(1f);

        dt.TriggerDialogue();
    }
}
