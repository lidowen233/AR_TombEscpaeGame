using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateNextHintForCatPhase : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject collectPanel;
    
    public GameManager _gameManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateNextHint()
    {
        collectPanel.SetActive(false);
        _gameManager.UpdateCatPhaseHints();
    }
}
