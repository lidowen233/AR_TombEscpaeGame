using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ActorEnum
{
    ProfessorActor,
    YouActor
};

public enum DisplayEnum
{
    DisplayAll,
    DisplayOneAtATime
}

[System.Serializable]
public class CustomEventType : UnityEvent { }

[System.Serializable]
public class Message
{
    public ActorEnum actor = ActorEnum.ProfessorActor;

    public AudioClip sentenceAudio;

    [TextArea(3, 10)]
    public string sentence;
}


[System.Serializable]
public class Dialogue
{
    public DisplayEnum displayMode = DisplayEnum.DisplayOneAtATime;

    public string lastMessageCTA;

    public CustomEventType onEndDialogue;

    public Message[] messages;
}

