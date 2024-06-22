// Copyright 2019-2021 Robotec.ai.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
namespace ROS2
{

/// <summary>
/// An example class provided for testing of basic ROS2 communication
/// </summary>
public class Injection_Motor_Calibration_Publisher : MonoBehaviour
{
    // Start is called before the first frame update
    private ROS2UnityComponent ros2Unity;
    private ROS2Node ros2Node;
    private IPublisher<std_msgs.msg.Empty> IJ_Motor_Cal_pub;
    [SerializeField] Button Injection_Motor_Calibration;
    private Image buttonImage_Injection_Motor_Calibration;
    private Color btnColor1 = Color.white;
    private Color btnColor2 = Color.red;
    private int i;
    void Awake(){
        // ROS_DOMAIN_IDを使う場合はこの行で設定する
        Environment.SetEnvironmentVariable("ROS_DOMAIN_ID", "24");
    }
    void Start()
    {
        ros2Unity = GetComponent<ROS2UnityComponent>();
        buttonImage_Injection_Motor_Calibration = Injection_Motor_Calibration.GetComponent<Image>();
        if(ros2Node == null){
            ros2Node = ros2Unity.CreateNode("Motor_Calibration");
            IJ_Motor_Cal_pub = ros2Node.CreatePublisher<std_msgs.msg.Empty>("Motor_Calibration");

            Injection_Motor_Calibration.onClick.AddListener(Publish_IJ_Cal);

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

    public void Publish_IJ_Cal(){
        var gamepad = Gamepad.current;
        if(gamepad != null && gamepad.buttonSouth.wasPressedThisFrame){
            return;
        }
        if (ros2Unity.Ok()){
            btnColor_reset();
            buttonImage_Injection_Motor_Calibration.color = btnColor2;
            std_msgs.msg.Empty msg = new std_msgs.msg.Empty();
            IJ_Motor_Cal_pub.Publish(msg);

        }
    }
    private void btnColor_reset(){
        buttonImage_Injection_Motor_Calibration.color = btnColor1;
    }
}

}  // namespace ROS2