using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAidsSimple : MonoBehaviour
{
    public GameObject dialogObject;
    public GameManagerSimple _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowNotes()
    {
        dialogObject.SetActive(true);
        _gameManager.WriteNotes();
    }
}
