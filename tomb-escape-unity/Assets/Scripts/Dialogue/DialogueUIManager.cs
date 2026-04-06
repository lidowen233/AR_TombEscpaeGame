using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject DialogueContainer;

    [SerializeField]
    private TextMeshProUGUI DialogueActorName;

    [SerializeField]
    private Image DialogueActorImage;

    [SerializeField]
    private TextMeshProUGUI DialogueText;

    [SerializeField]
    private GameObject NextButtonGO;

    [SerializeField]
    private GameObject CTAButtonGO;

    [SerializeField]
    private Sprite ProfessorImage;

    [SerializeField]
    private Sprite YouImage;


    // private vars
    private TextMeshProUGUI NextButtonText;
    private TextMeshProUGUI CTAButtonText;



    // Start is called before the first frame update
    void Start()
    {
        NextButtonText = NextButtonGO.GetComponentsInChildren<TextMeshProUGUI>()[0];
        CTAButtonText = CTAButtonGO.GetComponentsInChildren<TextMeshProUGUI>()[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeUI(Dialogue dialogue)
    {
        // Show UI
        DialogueContainer.SetActive(true);
    }

    public void DisplaySentenceUI(Message currentMessage, string lastMessageCTA = "")
    {
        // Update UI to current sentence
        DialogueText.text = currentMessage.sentence;

        // Update actor image and name
        if (currentMessage.actor == ActorEnum.ProfessorActor)
        {
            DialogueActorImage.sprite = ProfessorImage;
            DialogueActorName.text = "Professor P";
            DialogueActorName.color = new Color32(253, 180, 65, 255);
            NextButtonText.color = new Color32(253, 180, 65, 255);

        }
        else
        {
            DialogueActorImage.sprite = YouImage;
            DialogueActorName.text = "You";
            DialogueActorName.color = new Color32(204, 125, 238, 255);
            NextButtonText.color = new Color32(204, 125, 238, 255);
        }

        if (lastMessageCTA != "")
        {
            // show CTA button
            NextButtonGO.SetActive(false);
            CTAButtonGO.SetActive(true);

            CTAButtonText.text = lastMessageCTA;
        }
        else
        {
            // show Next button
            NextButtonGO.SetActive(true);
            CTAButtonGO.SetActive(false);
        }
    }


    public void HideUI()
    {
        // Hide UI
        DialogueContainer.SetActive(false);
        Debug.Log("Hide UI");
    }
}
