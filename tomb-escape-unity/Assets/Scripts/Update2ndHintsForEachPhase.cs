using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Update2ndHintsForEachPhase : MonoBehaviour
{
    /// <summary>
    /// this script is used to trigger the second remind after close the boundary remind
    /// </summary>
    public GameManager _gameManager;
    public GameObject remindUI;
    public GameObject arrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateNextHint()
    {   
        _gameManager.Update2ndHintsForEachPhase();

        StartCoroutine(RevealNextHint());
        
    }
    
    IEnumerator RevealNextHint()
    {
        arrow.SetActive(false); 
        _gameManager.UpdateNextHindFor2nd();
        yield return new WaitForSeconds(1); 

        //remindUI.SetActive(false);
    }
}
