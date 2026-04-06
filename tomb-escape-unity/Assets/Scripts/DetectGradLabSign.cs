using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class DetectGradLabSign : MonoBehaviour
{
    [SerializeField]
    private string newScene;

    [SerializeField]
    private TextMeshProUGUI ScanGradLabText;

    [SerializeField]
    private TextMeshProUGUI CountdownText;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchToGameScene()
    {
        Debug.Log("SwitchToGameScene() Called");
        StartCoroutine(WaitBeforeChangingScene());
    }

    public string GetSceneName()
    {
        return newScene != null ? newScene : string.Empty;
    }


    IEnumerator WaitBeforeChangingScene()
    {
        TextVisibility.HideText(ScanGradLabText);
        TextVisibility.ShowText(CountdownText);

        // switch out text
        ScanGradLabText.enabled = false;

        CountdownText.enabled = true;
        CountdownText.text = "3";

        yield return new WaitForSeconds(1);
        CountdownText.text = "2";

        yield return new WaitForSeconds(1);
        CountdownText.text = "1";

        yield return new WaitForSeconds(1);

        // switch scene
        SceneManager.LoadScene(GetSceneName());

    }
}
