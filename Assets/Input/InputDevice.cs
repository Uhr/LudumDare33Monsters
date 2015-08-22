using UnityEngine;
using System.Collections;

public interface InputDevice
{


    float GetXAxis();
    float GetYAxis();
    bool GetButton(int buttonIndex);
    bool GetButtonDown(int buttonIndex);
    bool GetButtonUp(int buttonIndex);

}
