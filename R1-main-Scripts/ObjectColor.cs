using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectColor : MonoBehaviour {
    public GameObject gameobject1;
    // public GameObject gameobject2;
    // public GameObject gameobject3;
    // public GameObject gameobject4;
    // public GameObject gameobject5;
    // public GameObject gameobject6;
    // public GameObject gameobject7;
    // public GameObject gameobject8;
    // public GameObject gameobject9;
    // public GameObject gameobject10;
    // public GameObject gameobject11;
    // public GameObject gameobject12;

    // Use this for initialization
    void Start () {
        //オブジェクトの色を用意したMaterialの色に変更する
        gameobject1.GetComponent<Renderer>().material.color = Color.red;
    }

    // Update is called once per frame
    void Update () {

    }
}