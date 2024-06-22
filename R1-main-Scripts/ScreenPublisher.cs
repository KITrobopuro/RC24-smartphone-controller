using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ROS2
{
    /// <summary>
    /// An example class provided for testing of basic ROS2 communication
    /// </summary>
    public class ScreenPublisher : MonoBehaviour
    {
        private ROS2UnityComponent ros2Unity;
        private ROS2Node ros2Node;
        private IPublisher<std_msgs.msg.String> ScrennBtn_pub;
        [SerializeField] private Button[] H = new Button[13];
        [SerializeField] List<Button> buttons;

        private Image[] buttonImage_H;
        private Dictionary<Button, Image> buttonImages = new Dictionary<Button, Image>();

        private readonly Color btnColor1 = Color.white;
        private readonly Color btnColor2 = Color.red;

        void Awake()
        {
            Environment.SetEnvironmentVariable("ROS_DOMAIN_ID", "24");
        }

        void Start()
        {
            ros2Unity = GetComponent<ROS2UnityComponent>();

            buttonImage_H = new Image[H.Length];
            for (int i = 0; i < H.Length; i++)
            {
                buttonImage_H[i] = H[i].GetComponent<Image>();
                AddButtonListener(H[i], $"H{i}", buttonImage_H[i]);
            }

            foreach (Button btn in buttons)
            {
                buttonImages[btn] = btn.GetComponent<Image>();
                btn.onClick.AddListener(() => PublishMessage(btn, btn.name, buttonImages[btn]));
            }

            if (ros2Node == null)
            {
                ros2Node = ros2Unity.CreateNode("ScreenPublisher");
                ScrennBtn_pub = ros2Node.CreatePublisher<std_msgs.msg.String>("SCRN_info");
            }
        }

        private void OnEnable()
        {
            Input.multiTouchEnabled = false;
        }

        private void OnDisable()
        {
            Input.multiTouchEnabled = true;
        }

        private void AddButtonListener(Button button, string message, Image buttonImage)
        {
            buttonImages.Add(button, buttonImage);
            button.onClick.AddListener(() => PublishMessage(button, message, buttonImage));
        }

        private void PublishMessage(Button button, string message, Image buttonImage)
        {
            var gamepad = Gamepad.current;
            if (gamepad != null && gamepad.buttonSouth.wasPressedThisFrame) return;

            if (ros2Unity.Ok())
            {
                ResetButtonColors();
                buttonImage.color = btnColor2;

                std_msgs.msg.String msg = new std_msgs.msg.String { Data = message };
                ScrennBtn_pub.Publish(msg);
            }
        }

        private void ResetButtonColors()
        {
            foreach (var kvp in buttonImages)
            {
                Button button = kvp.Key;
                Image image = kvp.Value;

                if (!button.name.StartsWith("H"))
                {
                    image.color = btnColor1;
                }
            }
        }
    }
}
