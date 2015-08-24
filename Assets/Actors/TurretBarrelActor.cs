using UnityEngine;
using System.Collections;

public class TurretBarrelActor : Actor {
	[SerializeField]
	ProjectileAttack projAttack;

	[SerializeField]
	float delay = 3;

	[SerializeField]
	float speedUpPerSecond = 0.1f;

	float start;
	float nextAttack;

	void OnTriggerEnter2D(Collider2D collider)
	{
	}

	public override Vector2 GetLastAnimationMovementDirection()
	{
		float rot = Mathf.Deg2Rad * -transform.rotation.eulerAngles.z;
		return new Vector2(Mathf.Sin(rot), Mathf.Cos(rot));
	}

	void Start()
	{
		start = Time.time;
		calcNextAttack();
		GetComponentInChildren<SimpleProjectile>().gameObject.SetActive(false);
	}

	void calcNextAttack()
	{
		nextAttack = Time.time + delay * Random.Range(0.8f, 1.2f);
	}

	void Update()
	{
		delay *= (1 - Time.deltaTime * speedUpPerSecond);
		transform.Rotate(new Vector3(0,0,10*Time.deltaTime));
		if(Time.time >= nextAttack)
		{
			projAttack.performAction();
			calcNextAttack();
		}
	}
}
