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
    private Image buttonImageStart;
    private Image buttonImageReset;
    private Color btnColor1 = Color.red;
    private Color btnColor2 = Color.white;
    private float countdownTime = 180f;
    private bool isCounting = false;
    private bool isVibrating = false;
    private float vibrationDuration = 0.1f;

    void Start()
    {
        buttonImageStart = startButton.GetComponent<Image>();
        buttonImageReset = resetButton.GetComponent<Image>();
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
        BtnColorReset();
        buttonImageStart.color = btnColor1;
        isCounting = true;
    }

    void ResetCountdown()
    {
        isCounting = false;
        BtnColorReset();
        buttonImageReset.color = btnColor1;
        countdownTime = 180f;
        UpdateCountdownDisplay();
    }

    void UpdateCountdownDisplay()
    {
        int minutes = Mathf.FloorToInt(countdownTime / 60);
        int seconds = Mathf.FloorToInt(countdownTime % 60);
        if(seconds == 150 && !isVibrating){
            StartCoroutine(StartVibration());
        }
        else if(seconds == 120 && !isVibrating){
            StartCoroutine(StartVibration());
        }
        else if(seconds == 90 && !isVibrating){
            StartCoroutine(StartVibration());
        }
        else if(seconds == 60 && !isVibrating){
            StartCoroutine(StartVibration());
        }
        else if(seconds == 30 && !isVibrating){
            StartCoroutine(StartVibration());
        }
        else if(seconds == 1 && !isVibrating){
            StartCoroutine(StartVibration());
        }
        countdownText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }

    void BtnColorReset(){
        buttonImageStart.color = btnColor2;
        buttonImageReset.color = btnColor2;
    }
    IEnumerator StartVibration()
    {
        isVibrating = true;

        // Androidの場合はAndroidのAPIを使用して振動を実行
        #if UNITY_ANDROID && !UNITY_EDITOR
            Handheld.Vibrate();
        #else
            // その他のプラットフォームではHandheldクラスを使用して振動を実行
            Handheld.Vibrate();
        #endif

        // 振動時間待機
        yield return new WaitForSeconds(vibrationDuration);

        // 振動停止
        StopVibration();
    }
    void StopVibration()
    {
        isVibrating = false;

        // Androidの場合はAndroidのAPIを使用して振動を停止
        #if UNITY_ANDROID && !UNITY_EDITOR
            Handheld.Vibrate();
        #else
        // その他のプラットフォームでは振動停止のAPIがないため、何もしない
        #endif
    }
}
