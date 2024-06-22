using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using ROS2;

public class ScreenColorPublisher : MonoBehaviour
{
    private ROS2UnityComponent ros2Unity;
    private ROS2Node ros2Node;
    private IPublisher<std_msgs.msg.String> btnColorPublisher;
    [SerializeField] private Button[] buttons;
    [SerializeField] private TextMeshProUGUI[] colorTexts;
    [SerializeField] public TextMeshProUGUI lastcolortext;
    [SerializeField] private Button colorResetButton;
    [SerializeField] private Button saveButton;

    private Color[] btnColors = { new Color(0.5f, 0.0f, 0.5f), Color.red, Color.white };
    private string[] colorNames = { "purple", "red", "white" };
    private Color[] textColors = { Color.white, Color.white, Color.black }; // テキストの色
    private int[] colorStates;
    private int[] prevColorStates; // 一つ前の色状態を保持する配列
    private bool isSaveButtonClicked = false; // セーブボタンがクリックされたかどうかを追跡
    private bool saveFlag = false;
    private bool consecutiveSaveClick = false; // 二回連続でセーブボタンが押されたかどうかを追跡

    private void Awake()
    {
        Environment.SetEnvironmentVariable("ROS_DOMAIN_ID", "24");
    }

    private void Start()
    {
        ros2Unity = GetComponent<ROS2UnityComponent>();
        colorStates = new int[buttons.Length];
        prevColorStates = new int[buttons.Length]; // 初期化

        if (ros2Unity.Ok())
        {
            ros2Node = ros2Unity.CreateNode("ScreenColorPublisher");
            btnColorPublisher = ros2Node.CreatePublisher<std_msgs.msg.String>("obj_color");

            for (int i = 0; i < buttons.Length; i++)
            {
                int index = i;
                buttons[i].onClick.AddListener(() => OnButtonClick(index));
            }

            colorResetButton.onClick.AddListener(OnColorReset);
            saveButton.onClick.AddListener(OnSave);
        }

        LoadButtonStates(); // ゲーム開始時にボタンの状態をロード
    }

    private void OnButtonClick(int index)
    {
        if (isSaveButtonClicked)
        {
            // 同じ色のボタンが連続して押された場合はタッチを無効にする
            if (colorStates[index] == prevColorStates[index])
            {
                return;
            }

            int prevIndex = prevColorStates[index];
            string prevColorName = colorNames[prevIndex];
            if (prevColorName == "red" && colorStates[index] == 2) // 赤色から白色に変わる場合
            {
                lastcolortext.text = "purple";
            }
            else if (prevColorName == "purple" && colorStates[index] == 2) // 紫色から白色に変わる場合
            {
                lastcolortext.text = "red";
            }
        }
        if (saveFlag == false){
            PublishButtonColor(index);
        }

        if (colorStates[index] != 2 || !isSaveButtonClicked) // 次の色が白色でない場合またはセーブボタンが押されていない場合のみ更新
        {
            prevColorStates[index] = colorStates[index]; // 現在の色状態を保存
            colorStates[index] = (colorStates[index] + 1) % btnColors.Length;
        }

        buttons[index].image.color = btnColors[colorStates[index]];
        colorTexts[index].color = textColors[colorStates[index]]; // テキストの色を設定
        SaveButtonStates(); // ボタンがクリックされたときに状態をセーブ
    }

    private void PublishButtonColor(int index)
    {
        if (ros2Unity.Ok())
        {
            string buttonName = buttons[index].name;
            string colorName = colorNames[colorStates[index]];
            string message = $"{buttonName}_{colorName}";

            std_msgs.msg.String msg = new std_msgs.msg.String();
            msg.Data = message;
            btnColorPublisher.Publish(msg);
        }
    }

    private void OnColorReset()
    {
        ResetColors();
        SaveButtonStates(); // 色をリセットした後に状態をセーブ
        isSaveButtonClicked = false; // リセットボタンが押されたので、フラグをリセット
        consecutiveSaveClick = false; // 連続セーブフラグをリセット
        ColorResetPublisher();
    }
    private void ColorResetPublisher(){
        std_msgs.msg.String msg = new std_msgs.msg.String();
        msg.Data = "ResetColor";
        btnColorPublisher.Publish(msg);
    }

    private void OnSave()
    {
        if (consecutiveSaveClick)
        {
            return; // 二回連続で押された場合は何もしない
        }

        saveFlag = true;
        for (int i = 0; i < buttons.Length; i++)
        {
            prevColorStates[i] = colorStates[i]; // 現在の状態を保存
            colorStates[i] = 2; // 次の色を白色に設定
        }
        SaveButtonStates(); // 状態をセーブ
        isSaveButtonClicked = true; // セーブボタンが押されたことを記録
        consecutiveSaveClick = true; // 連続セーブフラグを設定
    }

    private void ResetColors()
    {
        saveFlag = false;
        for (int i = 0; i < buttons.Length; i++)
        {
            colorStates[i] = 0; // 初期状態は紫色
            buttons[i].image.color = btnColors[colorStates[i]];
            colorTexts[i].color = textColors[colorStates[i]]; // テキストの色を設定
        }
    }

    private void SaveButtonStates()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            PlayerPrefs.SetInt($"ButtonState_{i}", colorStates[i]);
        }
        PlayerPrefs.Save();
    }

    private void LoadButtonStates()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            colorStates[i] = PlayerPrefs.GetInt($"ButtonState_{i}", 0); // デフォルト値は0（紫色）
            buttons[i].image.color = btnColors[colorStates[i]];
            colorTexts[i].color = textColors[colorStates[i]];
        }
    }

    private void OnApplicationQuit()
    {
        SaveButtonStates(); // ゲーム終了時に状態をセーブ
    }
}
