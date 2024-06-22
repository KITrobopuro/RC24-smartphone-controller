using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using ROS2;
using TMPro.Examples;

namespace ROS2
{
    public class SeedlingState : MonoBehaviour
    {
        public TextMeshProUGUI tmpro4;
        public TextMeshProUGUI tmpro5;
        [SerializeField] public Button up;
        [SerializeField] public Button down;
        [SerializeField] public Button harvestdown;
        [SerializeField] public Button reset;
        [SerializeField] public Button[] buttons;

        private Image buttonImage_up;
        private Image buttonImage_down;
        private Image buttonImage_harvestdown;
        private Image buttonImage_reset;
        private Color btnColor1 = Color.red;
        private Color btnColor2 = Color.white;
        const int CountReset = 0;
        const int CountMax = 12;
        int count = 0;
        int count1 = 0;


        void Start(){

            foreach (Button button in buttons)
            {
                button.onClick.AddListener(() => CheckButtonColor(button));
            }
            buttonImage_up = up.GetComponent<Image>();
            buttonImage_down = down.GetComponent<Image>();
            buttonImage_harvestdown = harvestdown.GetComponent<Image>();
            buttonImage_reset = reset.GetComponent<Image>();
            up.onClick.AddListener(OnUp);
            down.onClick.AddListener(OnDown);
            harvestdown.onClick.AddListener(OnHarvestd);
            reset.onClick.AddListener(Reset);

        }
        void OnUp(){
            ColorReset();
            buttonImage_up.color = btnColor1;
            if(tmpro4 != null && count < 12 && count1 < 12){
                count += 1;
                count1 += 1;
                tmpro4.text = count.ToString();
                tmpro5.text = count1.ToString();
                if(count >= 12 && count1 >= 12){
                    count = CountMax;
                    count1 = CountMax;
                }
            }
            
        }
        void OnDown(){
            ColorReset();
            buttonImage_down.color = btnColor1;
            if (tmpro4 != null && count > 0){
                count -= 1;
                tmpro4.text = count.ToString();
                if(count <= 0){
                    count = CountReset;
                }
            }
            
        }
        void OnHarvestd(){
            ColorReset();
            buttonImage_harvestdown.color = btnColor1;
            if (tmpro5 != null && count1 > 0){
                count1 -= 1;
                tmpro5.text = count1.ToString();
                if(count1 <= 0){
                    count1 = CountReset;
                }
            }
        }
        void Reset(){
            ColorReset();
            buttonImage_reset.color = btnColor1;
            if(tmpro4 != null && tmpro5 != null){
                count = CountReset;
                count1 = CountReset;
                tmpro4.text = count.ToString();
                tmpro5.text = count1.ToString();
            }
        }
        void ColorReset(){
            buttonImage_down.color = btnColor2;
            buttonImage_reset.color = btnColor2;
            buttonImage_up.color = btnColor2;
            buttonImage_harvestdown.color = btnColor2;
        }
        void CheckButtonColor(Button button)
        {
            // ボタンの色が白かどうかをチェック
            if (button.image.color == Color.white && tmpro5 != null && count1 > 0)
            {
                // カウントを減らす
                count1 -= 1;
                tmpro5.text = count1.ToString();
                if(count1 <= 0){
                    count1 = CountReset;
                }
            }
        }
    }
}