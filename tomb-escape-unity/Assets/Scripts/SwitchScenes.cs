using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
public class SwitchScenes : MonoBehaviour
{
    [SerializeField]
    private string newScene;
    public GameManager _gameManager;
    public GameObject guideObject;
    public Text guideText;

    public AudioSource movementAudio;
    public AudioSource successSound;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        //_gameManager.FinishPit();
        //_gameManager.ShowNextPhase(2);
        guideObject.SetActive(true);
        guideText.text = "Welcome to Door 5, here you need to finish the Glyph to escape!";
        StartCoroutine(OpenGate());
        ///SceneManager.LoadScene("GlyphTracing");
        //SceneManager.LoadScene(GetSceneName());
    }
    
    private void OnTriggerExit(Collider other)
    {
        SceneManager.LoadScene("GlyphTracing 1");
        //return newScene != null ? newScene : string.Empty;
    }
    public string GetSceneName()
    {
        return newScene != null ? newScene : string.Empty;
    }
        IEnumerator OpenGate()
    {
        // Play Success Sound
        successSound.Play();
        // wait 2 seconds
        yield return new WaitForSeconds(2);
        // Play some rock moving audio
        movementAudio.Play();
        yield return new WaitForSeconds(2); 
        movementAudio.Stop();
    }
}
