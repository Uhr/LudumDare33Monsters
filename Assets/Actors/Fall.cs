using UnityEngine;
using System.Collections.Generic;

public class Fall : Action {
	[SerializeField]
	SpriteRenderer sprite;

	[SerializeField]
	float shrinkSpeed;

	[SerializeField]
	float duration;

	[SerializeField]
	SpriteRenderer shadow;

	[SerializeField]
	Actor actor;

	private bool isActive = false;

	private float start = 0;

	private Vector3 oldScale;

	override public void performAction()
	{
		if(!isActive)
		{
			start = Time.time;
			isActive = true;
			actor.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			shadow.gameObject.SetActive(false);
			oldScale = sprite.transform.localScale;
		}
	}
	override public bool BlocksInputMovement()
	{
		return true;
	}
	override public bool IsBlockedBy(List<Action> activeActions)
	{
		return false;
	}
	override public void ResetState()
	{

	}
	override public bool IsActive()
	{
		return isActive;
	}

	public void setActive()
	{
		isActive = true;
	}

	void Update()
	{
		if(isActive)
		{
			sprite.transform.localScale *= (1 -shrinkSpeed * Time.deltaTime);
			Debug.Log ((Time.time - start) + " <= " + duration);
			if(Time.time - start > duration)
			{
				actor.Die ();
				shadow.gameObject.SetActive(true);
				sprite.transform.localScale = oldScale;
				
				isActive = false;
			}
		}
	}
}
