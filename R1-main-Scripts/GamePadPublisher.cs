using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls; // 追加する名前空間
using std_msgs.msg;

namespace ROS2
{
    /// <summary>
    /// An example class provided for testing of basic ROS2 communication
    /// </summary>
    public class GamePadPublisher : MonoBehaviour
    {
        private ROS2UnityComponent ros2Unity;
        private ROS2Node ros2Node;
        private IPublisher<std_msgs.msg.String> gamepad_pub;

        void Awake()
        {
            // Set ROS_DOMAIN_ID if needed
            Environment.SetEnvironmentVariable("ROS_DOMAIN_ID", "24");
        }

        void Start()
        {
            ros2Unity = GetComponent<ROS2UnityComponent>();
            if (ros2Node == null)
            {
                ros2Node = ros2Unity.CreateNode("GamePadPublisher");
                gamepad_pub = ros2Node.CreatePublisher<std_msgs.msg.String>("main_pad");
            }
        }

        private void OnEnable()
        {
            Input.multiTouchEnabled = false; // Prevent touch inputs on mobile
        }

        private void OnDisable()
        {
            Input.multiTouchEnabled = true;
        }

        void Update()
        {
            if (ros2Unity.Ok())
            {
                var gamepad = Gamepad.current;
                if (gamepad != null)
                {
                    CheckButton(gamepad.buttonEast, "b");
                    CheckButton(gamepad.buttonNorth, "y");
                    CheckButton(gamepad.buttonSouth, "a");
                    CheckButton(gamepad.buttonWest, "x");
                    CheckButton(gamepad.startButton, "s");
                    CheckButton(gamepad.selectButton, "g");
                    CheckButton(gamepad.leftShoulder, "l1");
                    CheckButton(gamepad.rightShoulder, "r1");
                    CheckButton(gamepad.leftTrigger, "l2");
                    CheckButton(gamepad.rightTrigger, "r2");
                    CheckButton(gamepad.leftStickButton, "l3");
                    CheckButton(gamepad.rightStickButton, "r3");
                    CheckButton(gamepad.dpad.up, "up");
                    CheckButton(gamepad.dpad.right, "right");
                    CheckButton(gamepad.dpad.left, "left");
                    CheckButton(gamepad.dpad.down, "down");
                }
                else
                {
                    Debug.LogError("No gamepad found.");
                }
            }
        }

        private void CheckButton(ButtonControl button, string message)
        {
            if (button.wasPressedThisFrame)
            {
                PublishMessage(message);
            }
        }

        private void PublishMessage(string data)
        {
            std_msgs.msg.String msg = new std_msgs.msg.String
            {
                Data = data
            };
            gamepad_pub.Publish(msg);
        }
    }
}
