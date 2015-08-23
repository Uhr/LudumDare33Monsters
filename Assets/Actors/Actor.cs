using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {
	public InputDevice input;
	public float movementSpeed = 1;
	public Collider2D collider;
	public Action[] actions = new Action[4];
	public float hp = 50;

    [SerializeField]
	private Rigidbody2D rigidBody;

    [SerializeField]
    Animator animator;

	// Use this for initialization
	void Awake () {
        rigidBody = gameObject.AddComponent<Rigidbody2D>();
        rigidBody.fixedAngle = true;
	}
	
	// Update is called once per frame
	void Update () {
		rigidBody.velocity = Vector2.zero;

		Vector2 moveDir = new Vector2(input.GetXAxis(), input.GetYAxis()).normalized;
		moveDir *= movementSpeed;

		rigidBody.velocity += moveDir;

		for(int i = 0; i<actions.Length; i++) {
			if(actions[i] != null && input.GetButton(i)) {
				actions[i].performAction();
			}
		}


        animator.ResetTrigger("DoJump");
        animator.ResetTrigger("DoRoll");


        if (input.GetButtonDown(0))
        {
            animator.SetTrigger("DoJump");
        }
        if (input.GetButtonDown(1))
        {
            animator.SetTrigger("DoRoll");
        }



	}
}
