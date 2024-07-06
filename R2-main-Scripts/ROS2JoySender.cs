using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace ROS2
{
    public class ROS2JoySender : MonoBehaviour
    {
        private ROS2UnityComponent ros2Unity;
        private ROS2Node ros2Node;
        private IPublisher<sensor_msgs.msg.Joy> joyPublisher;

        // コントローラ用の変数
        Vector2 analog_r;
        Vector2 analog_l;

        void Awake()
        {
            // Set ROS_DOMAIN_ID if needed
            Environment.SetEnvironmentVariable("ROS_DOMAIN_ID", "24");
        }

        void Start()
        {
            ros2Unity = GetComponent<ROS2UnityComponent>();
        }

        void Update()
        {
            if (ros2Unity.Ok())
            {
                if (ros2Node == null)
                {
                    ros2Node = ros2Unity.CreateNode("ROS2UnityJoyNode");
                    joyPublisher = ros2Node.CreatePublisher<sensor_msgs.msg.Joy>("joystick");
                }

                var gamepad = Gamepad.current;
                if (gamepad != null)
                {
                    // Joyスティックの値を読み取り
                    analog_r = gamepad.rightStick.ReadValue();
                    analog_l = gamepad.leftStick.ReadValue();

                    // 現在時刻を取得
                    DateTime now = DateTime.UtcNow;

                    // Joyメッセージを作成
                    sensor_msgs.msg.Joy joyMsg = new sensor_msgs.msg.Joy
                    {
                        Header = new std_msgs.msg.Header
                        {
                            Stamp = new builtin_interfaces.msg.Time
                            {
                                Sec = (int)now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
                                Nanosec = (uint)(now.Ticks % TimeSpan.TicksPerSecond) * 100
                            },
                            Frame_id = "joy"
                        },
                        Axes = new float[]
                        {
                            analog_l.x,
                            analog_l.y,
                            analog_r.x,
                            analog_r.y
                        },
                        Buttons = new int[0]  // ボタンの状態も送信する場合はここに追加
                    };

                    // メッセージをパブリッシュ
                    joyPublisher.Publish(joyMsg);
                }
            }
        }
    }
}
