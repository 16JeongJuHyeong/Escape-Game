using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    [SerializeField] private GameObject keypad;
    [SerializeField] private string password;
    [SerializeField] private GameObject actionText;

    [Header("Sound")]
    public AudioSource Open;
    public AudioSource Fail;
    public AudioSource Access;
    private string input = null;
    private bool doorOpen = false;

    public void MouseClick(string buttonType)
    {
        if (buttonType == "one") { input = input + "1"; }
        if (buttonType == "two") { input = input + "2"; }
        if (buttonType == "three") { input = input + "3"; }
        if (buttonType == "four") { input = input + "4"; }
        if (buttonType == "five") { input = input + "5"; }
        if (buttonType == "six") { input = input + "6"; }
        if (buttonType == "seven") { input = input + "7"; }
        if (buttonType == "eight") { input = input + "8"; }
        if (buttonType == "nine") { input = input + "9"; }
        if (buttonType == "zero") { input = input + "0"; }
        if (buttonType == "green") { CheckPassword(); }
        if (buttonType == "red") { input = null; }
    }

    private void CheckPassword()
    {
        if(input == password)
        {
            doorOpen = true;
            Access.Play();
            Open.Play();
            actionText.SetActive(true);
            Destroy(actionText, 3);
        }
        else
        {
            Fail.Play();
        }
        input = null;
        if (doorOpen)
        {
            GameObject.Find("ExitDoor").transform.Find("ExitLeft").gameObject.SetActive(false);
            GameObject.Find("ExitDoor").transform.Find("ExitRight").gameObject.SetActive(false);

        }
    }
}
