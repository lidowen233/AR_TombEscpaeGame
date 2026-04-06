using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextVisibility
{
    public static void ShowText(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(true);
    }

    public static void HideText(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(false);
    }
}