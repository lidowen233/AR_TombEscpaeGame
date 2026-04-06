using UnityEngine;
using Vuforia;
using UnityEngine.UI;
public class InteractiveHitCreatePlatform : MonoBehaviour
{
    private bool startPlaced = false;
    private PlaneFinderBehaviour planeFinder;
    [SerializeField] private GameObject platformPrefab; 
    private HitTestResult LastHit;

    public Text m_MyText;

    void Start()
    {
        
        planeFinder = GetComponent<PlaneFinderBehaviour>();
        
    }
    void OnEnable()
    {
        planeFinder = GetComponent<PlaneFinderBehaviour>();
        
    }

    public void intersectionLocation(HitTestResult _intersection)
    {
        if(_intersection != null)
        {
            //m_MyText.text = _intersection.Position.x + " , " + _intersection.Position.y + " , " +_intersection.Position.z;
            LastHit = _intersection;
        }
    }
    
   

    public void createPlatform()
    {
        if(planeFinder.enabled)
                Instantiate(platformPrefab, LastHit.Position, LastHit.Rotation);
        
    }
   
}

