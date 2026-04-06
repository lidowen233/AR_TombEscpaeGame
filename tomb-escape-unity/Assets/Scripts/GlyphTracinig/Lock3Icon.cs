using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lock3Icon : MonoBehaviour
{
    public string secretLetter;
    private bool isSelected = false;

    [SerializeField]
    private Sprite inactiveImage;

    [SerializeField]
    private Sprite activeImage;

    private SpriteRenderer _renderer;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Returns true if the icon was selected
    public bool MaybeSelectIcon()
    {
        if (!isSelected)
        {
            isSelected = true;
            _renderer.sprite = activeImage;
            return true;
        }
        return false;
    }

    public void ResetIcon()
    {
        isSelected = false;
        _renderer.sprite = inactiveImage;
    }
}
