using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{

    private Camera _cam;
    [SerializeField] private Line _linePrefab;

    public const float RESOLUTION = 0.1f;

    private Line _currentLine;

    void Start()
    {
        _cam = Camera.main;

    }

    void Update()
    {

        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("Mouse Position in World: " + mousePos);


        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse clicked, creating line at position: " + mousePos);
            _currentLine = Instantiate(_linePrefab, mousePos, Quaternion.identity);
        }

        if (Input.GetMouseButton(0)) _currentLine.SetPosition(mousePos);


    }
}
