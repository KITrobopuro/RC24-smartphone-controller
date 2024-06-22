using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImageController : MonoBehaviour
{
    public Toggle toggle;
    public Image image;
    public Sprite sample1;
    public Sprite sample2;
    [SerializeField] Button a;
    [SerializeField] Button b;
    [SerializeField] Button c;
    [SerializeField] Button d;
    [SerializeField] Button e;
    [SerializeField] Button f;

    void Start(){
        //a.transform.position = new (-320f,90f,0f);
    }

    public void voluechange()
    {
        image.sprite = toggle.isOn ? sample2 : sample1;
        if(toggle.isOn){
            image.sprite=sample2;
        }else{
            image.sprite=sample1;
        }
    }
}
