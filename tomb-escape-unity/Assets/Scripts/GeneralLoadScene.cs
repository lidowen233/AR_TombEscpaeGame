using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralLoadScene : MonoBehaviour
{
    [SerializeField]
    private string newSceneName;

    public void LoadNewScene()
    {
        SceneManager.LoadScene(GetSceneName());
    }
    public string GetSceneName()
    {
        return newSceneName != null ? newSceneName : string.Empty;
    }
}
