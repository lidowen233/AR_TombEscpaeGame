using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CatGroupRemind : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject remindUI;
    public Text remindText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        remindUI.SetActive(true);
        remindText.text = "Wanna Leave?\n" +" Go back to collect the cat statues by solving the riddles.";
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("LEAVE...");
        remindUI.SetActive(false);    
    }
}
