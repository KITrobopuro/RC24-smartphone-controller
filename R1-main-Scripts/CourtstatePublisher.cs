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

namespace ROS2
{

/// <summary>
/// An example class provided for testing of basic ROS2 communication
/// </summary>
public class CourtstatePublisher : MonoBehaviour
{
    // Start is called before the first frame update
    private ROS2UnityComponent ros2Unity;
    private ROS2Node ros2Node;
    private IPublisher<std_msgs.msg.Bool> state_pub;

    void Awake()
    {
            // ROS_DOMAIN_IDを使う場合はこの行で設定する
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
                ros2Node = ros2Unity.CreateNode("CourtstatePublisher");
                state_pub = ros2Node.CreatePublisher<std_msgs.msg.Bool>("coat_color");
            }
            
            std_msgs.msg.Bool msg = new std_msgs.msg.Bool();
            msg.Data = true;
            state_pub.Publish(msg);
        }
    }
}

}  // namespace ROS2