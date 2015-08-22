using UnityEngine;
using System.Collections;

public class XBox360InputDevice : InputDevice
{


    private int gamepadIndex;
    private string xAxisIdentifier;
    private string yAxixIdentifier;


    public XBox360InputDevice(int gamepadIndex)
    {
        this.gamepadIndex = gamepadIndex;
        xAxisIdentifier = "Axis X Pad " + gamepadIndex;
        yAxixIdentifier = "Axis Y Pad " + gamepadIndex;
    }


    public float GetXAxis()
    {
        return Input.GetAxis(xAxisIdentifier);
    }

    public float GetYAxis()
    {
        return -Input.GetAxis(yAxixIdentifier);
    }

    public bool GetButton(int buttonIndex)
    {
        return Input.GetKey("joystick " + gamepadIndex + " button " + buttonIndex);
    }

    public bool GetButtonDown(int buttonIndex)
    {
        return Input.GetKeyDown("joystick " + gamepadIndex + " button " + buttonIndex);
    }


    public bool GetButtonUp(int buttonIndex)
    {
        return Input.GetKeyUp("joystick " + gamepadIndex + " button " + buttonIndex);
    }
}
