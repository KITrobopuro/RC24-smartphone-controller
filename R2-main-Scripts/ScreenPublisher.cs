using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ROS2
{
    public class ScreenPublisher : MonoBehaviour
    {
        private ROS2UnityComponent ros2Unity;
        private ROS2Node ros2Node;
        private IPublisher<std_msgs.msg.String> ScrennBtn_pub;

        [SerializeField] List<Button> buttons;

        private Dictionary<Button, Image> buttonImages = new Dictionary<Button, Image>();
        private Color btnColor1 = Color.white;
        private Color btnColor2 = Color.red;

        void Awake()
        {
            Environment.SetEnvironmentVariable("ROS_DOMAIN_ID", "25");
        }

        void Start()
        {
            ros2Unity = GetComponent<ROS2UnityComponent>();

            foreach (Button btn in buttons)
            {
                buttonImages[btn] = btn.GetComponent<Image>();
                btn.onClick.AddListener(() => PublishButton(btn));
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

        private void PublishButton(Button btn)
        {
            var gamepad = Gamepad.current;
            if (gamepad.buttonSouth.wasPressedThisFrame) return;

            if (ros2Unity.Ok())
            {
                ResetButtonColors();
                buttonImages[btn].color = btnColor2;
                std_msgs.msg.String msg = new std_msgs.msg.String { Data = btn.name };
                ScrennBtn_pub.Publish(msg);
            }
        }

        private void ResetButtonColors()
        {
            foreach (var image in buttonImages.Values)
            {
                image.color = btnColor1;
            }
        }
    }
} // namespace ROS2
