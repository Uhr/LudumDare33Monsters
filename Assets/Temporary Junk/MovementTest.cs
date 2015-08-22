using UnityEngine;
using System.Collections;

public class MovementTest : MonoBehaviour {


    InputDevice input = new XBox360InputDevice(1);
    [SerializeField]
    Rigidbody physicsBody;
 
	
	// Update is called once per frame
	void Update () {
        float xVelocity = input.GetXAxis(); 
        float yVelocity = input.GetYAxis();

        if (input.GetButton(1))
        {
            xVelocity *= 2;
            yVelocity *= 2;
        }

        physicsBody.velocity = new Vector3(xVelocity, yVelocity, 0);
	}
}
