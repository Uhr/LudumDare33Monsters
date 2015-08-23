using UnityEngine;
using System.Collections;

public class Jump : Action {
	public float jumpSpeed = 5;

	[SerializeField]
	private GameObject shadow;
	private Rigidbody2D rigidBody;

	private bool isJumping = false;
	
	public override void performAction() {
		if(!isJumping) {
			isJumping = true;
			rigidBody.velocity += new Vector2(0, jumpSpeed);
		}
	}

	void Start() {
		rigidBody = gameObject.GetComponent<Rigidbody2D>();
	}

	void Update() {
		if(!isJumping) {
			rigidBody.velocity += new Vector2(0, jumpSpeed);
		}
	}
}
