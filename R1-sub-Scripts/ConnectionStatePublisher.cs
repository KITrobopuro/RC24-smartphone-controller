using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ROS2
{
    /// <summary>
    /// An example class provided for testing of basic ROS2 communication
    /// </summary>
    public class ConnectionStatePublisher : MonoBehaviour
    {
        private ROS2UnityComponent ros2Unity;
        private ROS2Node ros2Node;
        private IPublisher<std_msgs.msg.Empty> connection_pub;
        public float timeout = 0.1F;
        private float timeElapsed;
        private bool deviceConnected = false;

        void Awake()
        {
            // ROS_DOMAIN_IDを使う場合はこの行で設定する
            Environment.SetEnvironmentVariable("ROS_DOMAIN_ID", "24");
        }

        void Start()
        {
            ros2Unity = GetComponent<ROS2UnityComponent>();
            if (ros2Node == null)
            {
                ros2Node = ros2Unity.CreateNode("ConnectionStatePublisher");
                connection_pub = ros2Node.CreatePublisher<std_msgs.msg.Empty>("connection_state");
            }
        }

        private void OnEnable()
        {
            Input.multiTouchEnabled = false; // ゲームパッドのボタンを押したときに、スマホの画面が押されるのを防ぐ
            InputSystem.onDeviceChange += OnDeviceChange;
        }

        private void OnDisable()
        {
            Input.multiTouchEnabled = true;
            InputSystem.onDeviceChange -= OnDeviceChange;
        }

        void Update()
        {
            timeElapsed += Time.deltaTime;

            if (ros2Unity.Ok() && deviceConnected && timeElapsed >= timeout)
            {
                std_msgs.msg.Empty msg = new std_msgs.msg.Empty();
                connection_pub.Publish(msg);
                timeElapsed = 0.0F;
            }
        }

        void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            if (change == InputDeviceChange.Added)
            {
                deviceConnected = true;
            }
            else if (change == InputDeviceChange.Removed)
            {
                deviceConnected = false;
            }
        }
    }
}
// namespace ROS2
