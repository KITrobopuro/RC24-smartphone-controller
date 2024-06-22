using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsolController : MonoBehaviour
{
    public Toggle toggle;
    public GameObject ConsolePanel;

    // Update is called once per frame

    void Start()
    {
        ConsolePanel.SetActive(false);
    }
    public void consolchange()
    {
        if (toggle.isOn)
        {
            ConsolePanel.SetActive(true);
        }
        else
        {
            ConsolePanel.SetActive(false);
        }
    }
}