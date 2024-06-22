using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarvestReset : MonoBehaviour
{
    [SerializeField] private Button[] H = new Button[13];
    [SerializeField] private Button Reset;
    private Image[] buttonImage_H;
    private Color btnColorReset = Color.white;

    void Start(){
        buttonImage_H = new Image[H.Length];
        for (int i = 0; i < H.Length; i++)
        {
            buttonImage_H[i] = H[i].GetComponent<Image>();
        }
        Reset.onClick.AddListener(ResetColor);
    }

    void ResetColor(){
        foreach (var image in buttonImage_H)
        {
            if (image != null)
            {
                image.color = btnColorReset;
            }
        }
    }
}
