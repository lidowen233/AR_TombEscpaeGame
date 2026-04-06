using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class escapeDialog : MonoBehaviour
{
    public GameObject remindUI;
    public Text remindText;

    public GameObject FinalScene;

    public GameObject debugbtn;
    public GameObject mapbtn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MainCamera")
        {
            Debug.Log("tttttt");
            remindUI.SetActive(true);
            remindText.text = "Congratulations! Now it's time to go back to the light! ";
            mapbtn.SetActive(false);
            debugbtn.SetActive(false);
            StartCoroutine(OpenGate());
        }
    }
    IEnumerator OpenGate()
    {
        yield return new WaitForSeconds(2);
        FinalScene.SetActive(true);
      
        
    }
}
