using UnityEngine;
using System.Collections;

public class TurretProjectileAttack : ProjectileAttack {

	public override void performAction()
	{
		if(!gameObject.activeSelf)
			return;
		
		if (Time.time - lastActionTime > cooldown)
		{
			GameObject spawnedProjectile = Instantiate(projectile) as GameObject;
			spawnedProjectile.transform.position = projectile.transform.position;
			Vector2 dir = player.GetLastAnimationMovementDirection();
			spawnedProjectile.GetComponent<Projectile>().Initialize(dir, player);
			lastActionTime = Time.time;

			float angle = (Mathf.Atan2(dir.y, -dir.x) + 1.75f*Mathf.PI) % (2*Mathf.PI);
			float baseAngle = Mathf.Floor(angle / (Mathf.PI/2)) * Mathf.PI/2;
			float delta = -(angle - 0.25f * Mathf.PI - baseAngle);

			spawnedProjectile.transform.Rotate(0,0,Mathf.Rad2Deg * delta);
		}
	}
}
