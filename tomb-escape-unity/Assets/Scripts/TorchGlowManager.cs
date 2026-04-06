using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class TorchGlowManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI glowValueUI;

    [SerializeField]
    private Sprite[] torchSprites;

    [SerializeField]
    private GameManager _gameManager;

    [SerializeField]
    private PhasePartManager phaseManager;

    public GameObject remindUI;
    public Text remindText;

    private float maxTorchGlow;
    private float currentTorchGlow;
    private float glowFill;
    private float glowDelta;

    private float lerpSpeed;
    private Image torchImage;
    private int currentSpriteNumber;

    private bool isTorchTargetInView;

    private ScaleUpAnimation animator;

    // Start is called before the first frame update
    void Start()
    {
        // torch glow ranges from 0 to 100
        currentTorchGlow = 0.0f;
        maxTorchGlow = 100.0f;
        glowDelta = 1.0f;

        // Fill represents % of torch glow (0.0 - 1.0)
        glowFill = 0.0f;

        // set up empty torch image
        torchImage = GetComponent<Image>();
        currentSpriteNumber = 0;

        // Assume torch is not in view when game starts
        isTorchTargetInView = false;

        animator = GetComponent<ScaleUpAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        glowValueUI.text = "GlowValue: " + glowFill;

        // lerpSpeed = 3.0f * Time.deltaTime;

        // updateGlowFill();


        if (isTorchTargetInView)
        {
            increaseTorchGlow();
        }

    }

    private void increaseTorchGlow()
    {
        currentTorchGlow += glowDelta;

        if (currentTorchGlow > maxTorchGlow)
        {
            currentTorchGlow = maxTorchGlow;
        }

        updateGlowFill();
    }

    private void decreaseTorchGlow()
    {
        currentTorchGlow -= glowDelta;

        if (currentTorchGlow < 0)
        {
            currentTorchGlow = 0;
        }

        updateGlowFill();

    }

    private void updateGlowFill()
    {
        this.glowFill = currentTorchGlow / maxTorchGlow;

        //glowFill = Mathf.Lerp(glowFill, currentTorchGlow / maxTorchGlow, lerpSpeed);

        updateSpriteImage(glowFill);

        isTorchMax(glowFill);
    }

    /*  
        Maps the glow value to a sprite image

        @arg glowValue - a number betweeen 0 and 1
    */
    private void updateSpriteImage(float glowValue)
    {
        int numberOfSprites = torchSprites.Length;

        int spriteNumber = (int)Mathf.Floor(Mathf.Lerp(0, numberOfSprites, glowValue));

        if (spriteNumber > torchSprites.Length - 1) spriteNumber = torchSprites.Length - 1;

        if (currentSpriteNumber != spriteNumber)
        {
            torchImage.sprite = torchSprites[spriteNumber];
            currentSpriteNumber = spriteNumber;
        }
    }

    public void enableTorchInView()
    {
        isTorchTargetInView = true;
        animator.StartScalingUp();
    }

    public void disableTorchInView()
    {
        isTorchTargetInView = false;
        animator.ResetScale();
    }

    private void isTorchMax(float glowFill)
    {
        if (glowFill >= .99)
        {
            remindUI.SetActive(true);
            remindText.text = "You have finished charging the torch, try to light up the room.";
            phaseManager.UpdateTorchPhase(TorchPhase.LightPlayerTorch);
            _gameManager.realignRoom();
        }
    }
}
