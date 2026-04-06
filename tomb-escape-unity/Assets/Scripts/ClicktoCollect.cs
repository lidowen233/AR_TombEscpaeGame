using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ClicktoCollect : MonoBehaviour
{
    public CatManager catManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClicktoCollectCat()
    {
        catManager.AddCat();
    }
}
