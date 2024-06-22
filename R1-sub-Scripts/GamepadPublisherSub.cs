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
public class GamepadPublisherSub : MonoBehaviour
{
    // Start is called before the first frame update
    private ROS2UnityComponent ros2Unity;
    private ROS2Node ros2Node;
    private IPublisher<std_msgs.msg.String> gamepad_pub;
    private int i;

    void Awake(){
        // ROS_DOMAIN_IDを使う場合はこの行で設定する
        Environment.SetEnvironmentVariable("ROS_DOMAIN_ID", "24");
    }
    void Start()
    {
        ros2Unity = GetComponent<ROS2UnityComponent>();
        if (ros2Node == null){
                ros2Node = ros2Unity.CreateNode("GamePadPublisher");
                gamepad_pub = ros2Node.CreatePublisher<std_msgs.msg.String>("sub_pad");
        }
    }

    private void OnEnable()
    {
        Input.multiTouchEnabled = false;//ゲームパッドのボタンを押したときに、スマホの画面が押されるのを防ぐ
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

            if(gamepad.buttonEast.wasPressedThisFrame){
                std_msgs.msg.String msg = new std_msgs.msg.String();
                msg.Data += "b";
                gamepad_pub.Publish(msg);
            }
            if(gamepad.buttonNorth.wasPressedThisFrame){
                std_msgs.msg.String msg = new std_msgs.msg.String();
                msg.Data += "y";
                gamepad_pub.Publish(msg);
            }
            //なぜかscreen側のボタンと同期されるため封印
            if(gamepad.buttonSouth.wasPressedThisFrame){
                std_msgs.msg.String msg = new std_msgs.msg.String();
                msg.Data += "a";
                gamepad_pub.Publish(msg);
            }
            if(gamepad.buttonWest.wasPressedThisFrame){
                std_msgs.msg.String msg = new std_msgs.msg.String();
                msg.Data += "x";
                gamepad_pub.Publish(msg);
            }
            if(gamepad.startButton.wasPressedThisFrame){
                std_msgs.msg.String msg = new std_msgs.msg.String();
                msg.Data += "s";
                gamepad_pub.Publish(msg);
            }
            if(gamepad.selectButton.wasPressedThisFrame){
                std_msgs.msg.String msg = new std_msgs.msg.String();
                msg.Data += "g";
                gamepad_pub.Publish(msg);
            }
            if(gamepad.leftShoulder.wasPressedThisFrame){
                std_msgs.msg.String msg = new std_msgs.msg.String();
                msg.Data += "l1";
                gamepad_pub.Publish(msg);
            }
            if(gamepad.rightShoulder.wasPressedThisFrame){
                std_msgs.msg.String msg = new std_msgs.msg.String();
                msg.Data += "r1";
                gamepad_pub.Publish(msg);
            }
            if(gamepad.leftTrigger.wasPressedThisFrame){
                std_msgs.msg.String msg = new std_msgs.msg.String();
                msg.Data += "l2";
                gamepad_pub.Publish(msg);
            }
            if(gamepad.rightTrigger.wasPressedThisFrame){
                std_msgs.msg.String msg = new std_msgs.msg.String();
                msg.Data += "r2";
                gamepad_pub.Publish(msg);
            }
            if(gamepad.leftStickButton.wasPressedThisFrame){
                std_msgs.msg.String msg = new std_msgs.msg.String();
                msg.Data += "l3";
                gamepad_pub.Publish(msg);
            }
            if(gamepad.rightStickButton.wasPressedThisFrame){
                std_msgs.msg.String msg = new std_msgs.msg.String();
                msg.Data += "r3";
                gamepad_pub.Publish(msg);
            }
            if(gamepad.dpad.up.wasPressedThisFrame){
                std_msgs.msg.String msg = new std_msgs.msg.String();
                msg.Data += "up";
                gamepad_pub.Publish(msg);
            }
            if(gamepad.dpad.right.wasPressedThisFrame){
                std_msgs.msg.String msg = new std_msgs.msg.String();
                msg.Data += "right";
                gamepad_pub.Publish(msg);
            }
            if(gamepad.dpad.left.wasPressedThisFrame){
                std_msgs.msg.String msg = new std_msgs.msg.String();
                msg.Data += "left";
                gamepad_pub.Publish(msg);
            }
            if(gamepad.dpad.down.wasPressedThisFrame){
                std_msgs.msg.String msg = new std_msgs.msg.String();
                msg.Data += "down";
                gamepad_pub.Publish(msg);
            }
        }
    }
}

}  // namespace ROS2
