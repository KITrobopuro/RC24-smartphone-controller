using System;
using UnityEngine;
using ROS2;
using TMPro;
using System.Text;
using TMPro.Examples;

namespace ROS2
{
    public class Subscriber : MonoBehaviour
    {
        private ROS2UnityComponent ros2Unity;
        private ROS2Node ros2Node;
        private ISubscription<std_msgs.msg.String> chatterSub;
        private ISubscription<std_msgs.msg.Bool> chatterSub_1;
        private ISubscription<std_msgs.msg.Bool> chatterSub_2;
        private ISubscription<std_msgs.msg.Empty> chatterSub_3;
        
        // TextMeshProのテキスト要素
        public TextMeshProUGUI tmpro1; 
        public TextMeshProUGUI tmpro2;
        public TextMeshProUGUI tmpro3;


        //base_controller
        private string base_initial = "";
        private bool base_restart = false;
        private bool base_emergency = false;
        private bool base_move_auto = false;
        private bool base_state_communication = false;

        //convergence
        private bool con_spline = false;
        private bool con_arm = false;
        private bool con_net = false;
        private DateTime lastStateCommunicationTime;    // 最後にメッセージを受信した時刻を保持する変数を追加
        private float YOUR_TIMEOUT_SECONDS = 0.1F;

        void Awake(){
            // ROS_DOMAIN_IDを使う場合はこの行で設定する
            Environment.SetEnvironmentVariable("ROS_DOMAIN_ID", "25");
        }
        void Start()
        {
            lastStateCommunicationTime = DateTime.Now;  //タイムスタンプを初期化
            ros2Unity = GetComponent<ROS2UnityComponent>();
        }

        void Update()
        {
            if (ros2Node == null && ros2Unity.Ok())
            {
                //node生成
                ros2Node = ros2Unity.CreateNode("Subscriber");

                //base_controlのtopic
                chatterSub = ros2Node.CreateSubscription<std_msgs.msg.String>("initial_state_unity",msg => base_initial = msg.Data);
                chatterSub_1 = ros2Node.CreateSubscription<std_msgs.msg.Bool>("restart_unity",msg => base_restart = msg.Data);
                chatterSub_1 = ros2Node.CreateSubscription<std_msgs.msg.Bool>("emergency_unity",msg => base_emergency = msg.Data);
                chatterSub_1 = ros2Node.CreateSubscription<std_msgs.msg.Bool>("move_autonomous_unity",msg => base_move_auto = msg.Data);

                //convergenceのtopic
                chatterSub_2 = ros2Node.CreateSubscription<std_msgs.msg.Bool>("spline_convergence_unity",msg => con_spline = msg.Data);
                chatterSub_2 = ros2Node.CreateSubscription<std_msgs.msg.Bool>("arm_convergence_unity",msg => con_arm = msg.Data);
                chatterSub_2 = ros2Node.CreateSubscription<std_msgs.msg.Bool>("net_convergence_unity",msg => con_net = msg.Data);

                chatterSub_3 = ros2Node.CreateSubscription<std_msgs.msg.Empty>("state_communication_unity",msg => {

                    base_state_communication = true;    //メッセージを受信したらtrueに設定
                    lastStateCommunicationTime = DateTime.Now;  // タイムスタンプを更新
                    }
                );

            }


        }
        void LateUpdate()
        {
            // メインスレッドでTextMeshProのテキストを更新
            if (tmpro1 != null)
            {
                // sb = new StringBuilder();
                // sb.AppendLine("initial_state" +base_initial.ToString());
                // sb.AppendLine("move_auto" +base_restart.ToString());
                tmpro1.text = "initial_state    :" +base_initial + 
                    "\nrestart           :"+base_restart.ToString() + 
                    "\nemer             :" + base_emergency.ToString() +
                    "\nmove_auto    :" +base_move_auto.ToString();
            }
            if(tmpro2 != null){
                //ToStringでbool型をstring型に変換
                tmpro2.text = "spline   :"+con_spline.ToString()+
                    "\narm      :" + con_arm.ToString()+
                    "\nnet       :" + con_net.ToString();
            }
            if (tmpro3 != null)
            {
                if ((DateTime.Now - lastStateCommunicationTime).TotalSeconds > YOUR_TIMEOUT_SECONDS)
                {
                    base_state_communication = false;
                    tmpro3.text = "State Communication: False";
                    tmpro1.text = "initial_state    :" +base_initial + 
                    "\nrestart           :"+base_restart.ToString() + 
                    "\nemer             :True"+
                    "\nmove_auto    :" +base_move_auto.ToString();
                }
                else
                {
                    tmpro3.text = "State Communication: True";
                }            
            }
        }
    }
}
