using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public Button startButton;
    public Button resetButton;

    private float countdownTime = 180f;
    private bool isCounting = false;

    void Start()
    {
        startButton.onClick.AddListener(StartCountdown);
        resetButton.onClick.AddListener(ResetCountdown);
    }

    void Update()
    {
        if (isCounting)
        {
            if (countdownTime > 0)
            {
                countdownTime -= Time.deltaTime;
                UpdateCountdownDisplay();
            }
            else
            {
                countdownTime = 0f;
                isCounting = false;
                UpdateCountdownDisplay();
            }
        }
    }

    void StartCountdown()
    {
        var gamepad = Gamepad.current;
        if(gamepad.buttonSouth.wasPressedThisFrame){
            return;
        }
        isCounting = true;
    }

    void ResetCountdown()
    {
        var gamepad = Gamepad.current;
        if(gamepad.buttonSouth.wasPressedThisFrame){
            return;
        }
        isCounting = false;
        countdownTime = 180f;
        UpdateCountdownDisplay();
    }

    void UpdateCountdownDisplay()
    {
        int minutes = Mathf.FloorToInt(countdownTime / 60);
        int seconds = Mathf.FloorToInt(countdownTime % 60);
        countdownText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }
}
