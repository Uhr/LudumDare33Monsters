using UnityEngine;
using System.Collections;

public class KeyboardInputDevice : InputDevice
{
	KeyCode left, right, up, down, jump, dash, attack;

	public KeyboardInputDevice() : this(0)
	{

	}

	public KeyboardInputDevice(int id)
	{
		switch(id)
		{
		case 1:
			Init (
				KeyCode.LeftArrow,
				KeyCode.RightArrow,
				KeyCode.UpArrow,
				KeyCode.DownArrow,
				KeyCode.P,
				KeyCode.O,
				KeyCode.I);
			break;

			
		default:
			Init (
				KeyCode.A,
				KeyCode.D,
				KeyCode.W,
				KeyCode.S,
				KeyCode.C,
				KeyCode.V,
				KeyCode.B);
			break;
		}
	}

	private void Init(KeyCode l, KeyCode r, KeyCode u, KeyCode d,
	                  KeyCode jmp, KeyCode dsh, KeyCode atk)
	{
		left = l;
		right = r;
		up = u;
		down = d;
		jump = jmp;
		dash = dsh;
		attack = atk;
	}

    override public float GetXAxis()
    {
        float x = 0;

        if (Input.GetKey(left))
            x -= 1;

        if (Input.GetKey(right))
            x += 1;

        return x;
    }

    override public float GetYAxis()
    {
        float y = 0;

        if (Input.GetKey(down))
            y -= 1;

        if (Input.GetKey(up))
            y += 1;

        return y;
    }

    override public bool GetButton(int buttonIndex)
    {
        switch (buttonIndex)
        {
            case 0: return Input.GetKey(jump);
            case 1: return Input.GetKey(dash);
            case 2: return Input.GetKey(attack);
            case 7: return Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Escape);

        }

        return false;
    }

    override public bool GetButtonDown(int buttonIndex)
    {
        switch (buttonIndex)
        {
            case 0: return Input.GetKeyDown(jump);
            case 1: return Input.GetKeyDown(dash);
            case 2: return Input.GetKeyDown(attack);
            case 7: return Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape);
        }

        return false;
    }


    override public bool GetButtonUp(int buttonIndex)
    {
        switch (buttonIndex)
        {
            case 0: return Input.GetKeyUp(jump);
            case 1: return Input.GetKeyUp(dash);
            case 2: return Input.GetKeyUp(attack);
            case 7: return Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Escape);
        }

        return false;
    }
}
