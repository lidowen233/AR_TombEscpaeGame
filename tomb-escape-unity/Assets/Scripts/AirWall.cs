using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirWall : MonoBehaviour
{
    Collider airWallCollider;  
    private bool canPass = false;     
    public Text m_MoveText;
    public GameManager _gameManager;

    void Start()
    {
        airWallCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (_gameManager.GetCurrentState() == GameManager.currentState.Finished)
        {
            AllowPassThrough(); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("..........." + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("MainCamera"))  
        {
            Debug.Log("...........");
            if (!canPass)
            {
                
                m_MoveText.text = "You cannot pass yet!";
            }
            else
            {
                
                 m_MoveText.text = "You can pass!";
            }
        }
    }

    public void AllowPassThrough()
    {
        canPass = true; 
        airWallCollider.enabled = false; 
        Debug.Log("Air wall disabled, you can pass now.");
    }

    public void BlockPassThrough()
    {
        canPass = false; 
        airWallCollider.enabled = true; 
        Debug.Log("Air wall enabled, you cannot pass yet.");
    }
}
