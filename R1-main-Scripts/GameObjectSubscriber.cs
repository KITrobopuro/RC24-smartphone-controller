using System;
using UnityEngine;
using ROS2;
using std_msgs.msg;

namespace ROS2
{
    public class GameObjectSubscriber : MonoBehaviour
    {
        private ROS2UnityComponent ros2Unity;
        private ROS2Node ros2Node;
        private ISubscription<std_msgs.msg.String> GameObjectSub;
        private string objectcolor = "";
        public GameObject[] gameObjects;

        void Awake()
        {
            Environment.SetEnvironmentVariable("ROS_DOMAIN_ID", "24");
        }

        void Start()
        {
            ros2Unity = GetComponent<ROS2UnityComponent>();

            // デフォルト色を紫色に設定
            SetDefaultColorsToPurple();
        }

        void Update()
        {
            if (ros2Unity != null && ros2Unity.Ok() && ros2Node == null)
            {
                ros2Node = ros2Unity.CreateNode("GameObjectSubscriber");
                GameObjectSub = ros2Node.CreateSubscription<std_msgs.msg.String>("obj_color", msg => 
                {
                    objectcolor = msg.Data;
                    if (objectcolor == "ResetColor"){
                        SetDefaultColorsToPurple();
                    }
                    UpdateGameObjectColors(objectcolor);
                    
                });
            }
        }

        void SetDefaultColorsToPurple()
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (gameObjects[i] != null && gameObjects[i].GetComponent<Renderer>() != null)
                {
                    gameObjects[i].GetComponent<Renderer>().material.color = new Color(0.5f, 0.0f, 0.5f); // 紫色に設定
                }
                else
                {
                    Debug.LogError($"GameObject or Renderer component at index {i} is null.");
                }
            }
        }

        void UpdateGameObjectColors(string colorCode)
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (gameObjects[i] != null && gameObjects[i].GetComponent<Renderer>() != null)
                {
                    SetGameObjectColor(gameObjects[i], colorCode, i);
                }
                else
                {
                    Debug.LogError($"GameObject or Renderer component at index {i} is null.");
                }
            }
        }
        void SetGameObjectColor(GameObject gameObject, string colorCode, int objectIndex)
        {
            string expectedPrefix = $"H{objectIndex}_";
            if (colorCode.StartsWith(expectedPrefix))
            {
                string colorName = colorCode.Substring(expectedPrefix.Length);
                switch (colorName)
                {
                    case "purple":
                        gameObject.GetComponent<Renderer>().material.color = Color.red;
                        break;
                    case "white":
                        gameObject.GetComponent<Renderer>().material.color = new Color(0.5f, 0.0f, 0.5f);
                        break;
                    // 他の色も追加可能
                    default:
                        Debug.LogWarning($"Unknown color code: {colorName}");
                        break;
                }
            }
        }
    }
}
