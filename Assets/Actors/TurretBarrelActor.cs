using UnityEngine;
using System.Collections;

public class TurretBarrelActor : Actor {
	[SerializeField]
	ProjectileAttack projAttack;

	[SerializeField]
	float delay = 3;

	[SerializeField]
	float speedUpPerSecond = 0.1f;

	[SerializeField]
	float minDelay = 0.5f;

	[SerializeField]
	float rotSpeed = 10f;

	[SerializeField]
	bool limitedRot = false;

	[SerializeField]
	float minRot;

	[SerializeField]
	float maxRot;

	float nextAttack;

	private float startRot;
	private float rotOffset = 0f;

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
		calcNextAttack();
		startRot = transform.localEulerAngles.z;
		GetComponentInChildren<SimpleProjectile>().gameObject.SetActive(false);
	}

	void calcNextAttack()
	{
		nextAttack = Time.time + delay * Random.Range(0.7f, 1.3f);
	}

	void Update()
	{
		delay *= (1 - Time.deltaTime * speedUpPerSecond);
		delay = Mathf.Max (delay, minDelay);

		if(limitedRot)
		{
			rotOffset += rotSpeed * Random.Range(0.7f, 1.3f) * Time.deltaTime;
			float rot = Mathf.PingPong(startRot + rotOffset, maxRot - minRot) + minRot;
			transform.localEulerAngles = new Vector3(0,0,rot);
		} else
			transform.Rotate(new Vector3(0,0,rotSpeed* Random.Range(0.7f, 1.3f)*Time.deltaTime));

		if(Time.time >= nextAttack)
		{
			projAttack.performAction();
			calcNextAttack();
		}
	}
}
