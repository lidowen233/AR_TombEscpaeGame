using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    public Text uiText;
    public string fullText; 
    public float typingSpeed;
    private string currentText;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (!string.IsNullOrEmpty(fullText)) 
        {
            StartCoroutine(ShowText());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string newText)
    {
        fullText = newText;
        currentText = ""; 
        uiText.text = ""; 
    }
    IEnumerator ShowText()
    {
        for(int i =0;i<=fullText.Length;i++)
        {
            currentText = fullText.Substring(0,i);
            uiText.text = currentText;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

}
