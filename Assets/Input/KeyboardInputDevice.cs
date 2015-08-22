using UnityEngine;
using System.Collections;

public class KeyboardInputDevice : InputDevice {

	override public float GetXAxis()
	{
		float x = 0;

		if(Input.GetKey(KeyCode.A))
			x -= 1;

		if(Input.GetKey(KeyCode.D))
			x += 1;

		return x;
	}
	
	override public float GetYAxis()
	{
		float y = 0;
		
		if(Input.GetKey(KeyCode.S))
			y -= 1;
		
		if(Input.GetKey(KeyCode.W))
			y += 1;
		
		return y;
	}
	
	override public bool GetButton(int buttonIndex)
	{
		switch(buttonIndex) {
			case 0: return Input.GetKey (KeyCode.J);
			case 1: return Input.GetKey (KeyCode.K);
			case 2: return Input.GetKey (KeyCode.L);
			case 3: return Input.GetKey (KeyCode.I);
		}

		return false;
	}
	
	override public bool GetButtonDown(int buttonIndex)
	{
		switch(buttonIndex) {
			case 0: return Input.GetKeyDown (KeyCode.J);
			case 1: return Input.GetKeyDown (KeyCode.K);
			case 2: return Input.GetKeyDown (KeyCode.L);
			case 3: return Input.GetKeyDown (KeyCode.I);
		}

		return false;
	}
	
	
	override public bool GetButtonUp(int buttonIndex)
	{
		switch(buttonIndex) {
			case 0: return Input.GetKeyUp (KeyCode.J);
			case 1: return Input.GetKeyUp (KeyCode.K);
			case 2: return Input.GetKeyUp (KeyCode.L);
			case 3: return Input.GetKeyUp (KeyCode.I);
		}

		return false;
	}
}
