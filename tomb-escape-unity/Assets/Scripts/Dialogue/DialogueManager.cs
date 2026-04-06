using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private DialogueUIManager DialogueUIManager;

    [SerializeField]
    private DialogueAudioManager DialogueAudioManager;

    private Queue<Message> messages;

    private CustomEventType onEndDialogue;

    private string lastMessageCTA;

    void Start()
    {
        messages = new Queue<Message>();
    }

    public void StartDialogue(Dialogue dialogue)
    {


        if (messages.Count > 0)
        {
            Debug.Log("Error... overriding an ongoing dialogue");
            messages.Clear();
        }

        DialogueUIManager.InitializeUI(dialogue);

        foreach (Message message in dialogue.messages)
        {
            messages.Enqueue(message);
        }

        onEndDialogue = dialogue.onEndDialogue;

        lastMessageCTA = dialogue.lastMessageCTA;

        switch (dialogue.displayMode)
        {
            case DisplayEnum.DisplayAll:
                LoopThroughAllSentences();
                break;
            case DisplayEnum.DisplayOneAtATime:
                DisplayNextSentence();
                break;
            default:
                LoopThroughAllSentences();
                break;
        }
    }

    // Loops through all sentences in one go
    private void LoopThroughAllSentences()
    {
        Debug.Log("now we have messages " + messages.Count);
        while (messages.Count > 0)
        {
            DisplaySentence();
        }
        EndDialogue();
        return;
    }

    // provides control over display next sentence (can be triggered by this script, gameplay or UI next button)
    public void DisplayNextSentence()
    {
        Debug.Log(messages.Count);
        if (messages.Count == 0)
        {
            EndDialogue();
        }
        else
        {
            DisplaySentence();
        }
    }

    private void DisplaySentence()
    {
        bool isLastMessage = messages.Count == 1;

        Message currentMessage = messages.Dequeue();
        Debug.Log(currentMessage.sentence);
        Debug.Log(isLastMessage);

        if (isLastMessage)
        {
            DialogueUIManager.DisplaySentenceUI(currentMessage, lastMessageCTA ?? string.Empty);
        }
        else
        {
            DialogueUIManager.DisplaySentenceUI(currentMessage);
        }

        DialogueAudioManager.PlaySentenceAudio(currentMessage);
    }

    private void EndDialogue()
    {
        DialogueUIManager.HideUI();
        Debug.Log("EndDialogue");

        if (onEndDialogue != null)
        {
            onEndDialogue.Invoke();
        }
    }

}
