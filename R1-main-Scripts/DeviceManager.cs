using UnityEngine;
using UnityEngine.InputSystem;

public class DeviceManager : MonoBehaviour
{
    private void Start()
    {
        InputDevice[] devices = InputSystem.devices.ToArray();
        foreach (InputDevice device in devices)
        {
            Debug.Log($"Device name: {device.name}, interface: {device.description.interfaceName}");
        }
    }
}