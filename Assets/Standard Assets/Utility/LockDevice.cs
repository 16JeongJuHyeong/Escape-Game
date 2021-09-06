using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;

public class LockDevice : MonoBehaviour
{
    public static bool DeviceActivated = false;
    [SerializeField] private GameObject keypad_base;

    private void Update()
    {
        TryOpenDevice();
    }
    private void OpenKeypad()
    {
        keypad_base.SetActive(true);
    }
    private void CloseKeypad()
    {
        keypad_base.SetActive(false);
    }
    public void TryOpenDevice()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            DeviceActivated = !DeviceActivated;
            if (DeviceActivated)
                OpenKeypad();
            else
                CloseKeypad();
        }
    }
}
