using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCat : MonoBehaviour
{
    public Camera arCamera; 
    
    //
    public CatManager _catManager;
    public int catIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   void Update()
    {
        bool isMouseDown = Input.GetMouseButtonDown(0);
        bool isPhoneTapped = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;

        if(isMouseDown )
        {
            Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                string catTag= $"cat{catIndex}";
                if (hit.transform.CompareTag(catTag))
                {
                    Debug.Log("clicked");
                    _catManager.CollectCat(catIndex);

                }
            }
        }
        else if(isPhoneTapped)
        {
            Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                string catTag= $"cat{catIndex}";
                if (hit.transform.CompareTag(catTag))
                {
                    Debug.Log("clicked");
                    _catManager.CollectCat(catIndex);

                }
            }
        }
        
    }
    
}
