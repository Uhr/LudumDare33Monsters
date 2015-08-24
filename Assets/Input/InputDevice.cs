using UnityEngine;
using System.Collections;

public abstract class InputDevice
{


    public abstract float GetXAxis();
    public abstract float GetYAxis();
    public abstract bool GetButton(int buttonIndex);
    public abstract bool GetButtonDown(int buttonIndex);
    public abstract bool GetButtonUp(int buttonIndex);

}
