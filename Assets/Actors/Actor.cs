using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {
	public InputDevice input;
	public float movementSpeed = 5;
	public float maxJumpHeight = 1;
	public float jumpSpeed = 2;

	private float currentJumpHeight = 0;
	private bool isJumping = false;
	private bool jumpGoingUp = true;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 moveDir = new Vector3(input.GetXAxis(), input.GetYAxis(), 0).normalized;
		moveDir *= movementSpeed * Time.deltaTime;

		if(!isJumping && input.GetButton(0)) {
			isJumping = true;
			jumpGoingUp = true;
			currentJumpHeight = 0;
		}

		if(isJumping) {
			float deltaHeight = Mathf.Clamp(
				jumpSpeed * (jumpGoingUp ? 1 : -1) * Time.deltaTime,
				-currentJumpHeight,
				maxJumpHeight - currentJumpHeight);

			moveDir.y += deltaHeight;
			currentJumpHeight += deltaHeight;

			if(currentJumpHeight <= 0) {
				isJumping = false;
			} else if(currentJumpHeight >= maxJumpHeight) {
				jumpGoingUp = false;
			}
		}

		
		transform.Translate(moveDir);
	}
}
