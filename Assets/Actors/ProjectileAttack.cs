using UnityEngine;
using System.Collections.Generic;

public class ProjectileAttack : Action
{
    private float lastActionTime;

    [SerializeField]
    private float cooldown;

    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private Actor player;

    public override void performAction()
    {
        if (Time.time - lastActionTime > cooldown)
        {
            GameObject spawnedProjectile = Instantiate(projectile) as GameObject;
            spawnedProjectile.transform.position = projectile.transform.position;
            spawnedProjectile.GetComponent<Projectile>().Initialize(player.GetLastAnimationMovementDirection());
        }
    }

    public override bool BlocksInputMovement()
    {
        return false;
    }

    public override bool IsBlockedBy(List<Action> activeActions)
    {
        return activeActions.Exists(x => x is Dash);
    }

    public override void ResetState()
    {
        throw new System.NotImplementedException();
    }

    public override bool IsActive()
    {
        return Time.time - lastActionTime <= cooldown;
    }
}
