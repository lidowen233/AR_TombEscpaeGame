using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackController : MonoBehaviour
{
    private SpriteRenderer _rend;

    [SerializeField]
    private Sprite correctSprite;

    [SerializeField]
    private Sprite wrongSprite;

    void Start()
    {
        _rend = GetComponent<SpriteRenderer>();
        _rend.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowCorrectSprite()
    {
        StartCoroutine(ShowSpriteForDelay(correctSprite, 4));
    }

    public void ShowWrongSprite()
    {
        StartCoroutine(ShowSpriteForDelay(wrongSprite, 2));
    }

    private IEnumerator ShowSpriteForDelay(Sprite sprite, int delay)
    {
        _rend.sprite = sprite;

        // Enable the sprite
        _rend.enabled = true;

        // Wait for specified number of seconds
        yield return new WaitForSeconds(delay);

        // Disable the sprite
        _rend.enabled = false;
    }

    public void HideFeedback()
    {
        _rend = GetComponent<SpriteRenderer>();
        _rend.enabled = false;
    }

}
