using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Net;
using System.Net.Sockets;
using System;
using System.Threading;
using TMPro;

public class UDPSender : MonoBehaviour
{
    //IPアドレスとポート番号
    [SerializeField] private string serverIP;
    [SerializeField] private int serverPort;

    //デバック用text
    public GameObject DebugText1 = null;
    public GameObject DebugText2 = null;
    public GameObject DebugText3 = null;
    TextMeshProUGUI tmpro1;
    TextMeshProUGUI tmpro2;
    TextMeshProUGUI tmpro3;

    //コントローラ用の変数
    Vector2 analog_r;
    Vector2 analog_l;

    //UDP用
    private UdpClient udpClient;
    private IPEndPoint endPoint;

    int count;

    void Start()
    {
        //UDP
        udpClient = new UdpClient();
        endPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
        //textのキャッシュ
        tmpro1 = DebugText1.GetComponent<TextMeshProUGUI>();
        tmpro2 = DebugText2.GetComponent<TextMeshProUGUI>();
        tmpro3 = DebugText3.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        var gamepad = Gamepad.current;
        
        //Joy代入
        analog_r = gamepad.rightStick.ReadValue();
        analog_l = gamepad.leftStick.ReadValue();

        //デバックtext
        //表示される値は小数点3位まで似設定した:F3
        tmpro1.text = $"l_x:{analog_l.y:F3}";
        tmpro2.text = $"l_y:{-analog_l.x:F3}";
        tmpro3.text = $"l_y:{analog_r.x:F3}";
        //tmpro7.text = $"l_x:{count}";

        //バイト変換、UDP送信
        byte[] data = new byte[16];
        //BlockCopy(元のデータを格納するオブジェクト,コピー操作の開始位置となるインデックス番号,データを受け取るオブジェクト,データ格納の開始するインデックス番号,コピーするデータの大きさ)
        Buffer.BlockCopy(new float[] { analog_l.x, analog_l.y, analog_r.x, analog_r.y }, 0, data, 0, data.Length);
        //元のデータを格納するオブジェクト,データの大きさ、IPアドレスとサーバーポート
        udpClient.SendAsync(data, data.Length, endPoint);    }

    void OnDisable()
    {
        //UDPの終了
        udpClient.Close();
    }
}
