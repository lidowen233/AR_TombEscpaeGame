using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleTextController : MonoBehaviour
{
    private TextMeshPro puzzleText;

    private const string ANSWER = "ESCAPE";

    // Start is called before the first frame update
    void Start()
    {
        puzzleText = GetComponent<TextMeshPro>();
        puzzleText.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetPuzzleText()
    {
        return puzzleText.text;
    }


    public void SetPuzzleText(string newLetter)
    {
        puzzleText.text += newLetter;
    }

    public void ResetPuzzleText()
    {
        puzzleText.text = "";
    }

    public bool CheckPuzzleText()
    {
        return string.Equals(puzzleText.text, ANSWER);
    }

}
