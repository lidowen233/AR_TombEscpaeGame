using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HandleTextContent : MonoBehaviour
{
    [SerializeField]
    //private Text TextContentUI;
    private TextMeshProUGUI TextContentUI;

    [SerializeField]
    private GameObject TextObject;

    [SerializeField]
    [TextArea(3, 10)]
    public string[] messages;

    private RectTransform textTransform;
    private Vector3 originalSize;

    // Start is called before the first frame update
    void Start()
    {
        TextContentUI.text = "";

        textTransform = TextObject.GetComponent<RectTransform>();
        originalSize = textTransform.localScale;

        HideText();
    }

    public void HideText()
    {
        textTransform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowText()
    {
       
        //TextObject.SetActive(true);
        textTransform.localScale = originalSize;
    }

    public void SetTextContent(int msgIndex)
    {
        TextContentUI.text = messages[msgIndex];
    }
}
