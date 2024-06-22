using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

namespace ROS2
{

/// <summary>
/// An example class provided for testing of basic ROS2 communication
/// </summary>
public class RestartPublisher : MonoBehaviour
{
    // Start is called before the first frame update
    private ROS2UnityComponent ros2Unity;
    private ROS2Node ros2Node;
    private IPublisher<std_msgs.msg.String> ScrennBtn_pub;
    [SerializeField] Button initialA;
    [SerializeField] Button initialO;
    private Image buttonImage_initialA;
    private Image buttonImage_initialO;
    private Color btnColor1 = Color.white;
    private Color btnColor2 = Color.red;


    void Awake(){
        // ROS_DOMAIN_IDを使う場合はこの行で設定する
        Environment.SetEnvironmentVariable("ROS_DOMAIN_ID", "25");
    }

    void Start()
    {
        ros2Unity = GetComponent<ROS2UnityComponent>();
        buttonImage_initialA = initialA.GetComponent<Image>();
        buttonImage_initialO = initialO.GetComponent<Image>();
        if (ros2Node == null){
            ros2Node = ros2Unity.CreateNode("RestartPublisher");
            ScrennBtn_pub = ros2Node.CreatePublisher<std_msgs.msg.String>("initial_state");
            
            initialA.onClick.AddListener(Publish_initialA);
            initialO.onClick.AddListener(Publish_initialO);
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
    public void Publish_initialA(){
        var gamepad = Gamepad.current;
        if(gamepad.buttonSouth.wasPressedThisFrame){
            return;
        }
        if (ros2Unity.Ok()){
            btnColor_reset();
            buttonImage_initialA.color = btnColor2;
            std_msgs.msg.String msg = new std_msgs.msg.String();
            msg.Data = "A";
            ScrennBtn_pub.Publish(msg);
        }
    }
    public void Publish_initialO(){
        var gamepad = Gamepad.current;
        if(gamepad.buttonSouth.wasPressedThisFrame){
            return;
        }
        if (ros2Unity.Ok()){
            btnColor_reset();
            buttonImage_initialO.color = btnColor2;
            std_msgs.msg.String msg = new std_msgs.msg.String();
            msg.Data = "O";
            ScrennBtn_pub.Publish(msg);
        }
    }
    private void btnColor_reset(){
        buttonImage_initialA.color = btnColor1;
        buttonImage_initialO.color = btnColor1;
    }
}
}