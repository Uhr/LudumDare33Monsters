using UnityEngine;
using System.Collections;

public class Jump : Action {
	public float jumpSpeed = 5;

	[SerializeField]
	private GameObject shadow;
	private bool isGrounded = false;
	private Rigidbody2D rigidBody;

	private bool isJumping = false;

	void Start() {
		rigidBody = gameObject.GetComponent<Rigidbody2D>();
	}

	public override void performAction() {
		if(isGrounded) {
			rigidBody.velocity += new Vector2(0, jumpSpeed);
		}
	}
}
