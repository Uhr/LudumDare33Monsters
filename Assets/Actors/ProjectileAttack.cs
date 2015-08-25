using UnityEngine;
using System.Collections.Generic;

public class ProjectileAttack : Action, PlayerChoiceReactor
{
    protected float lastActionTime;

    [SerializeField]
    protected float cooldown;

    [SerializeField]
    protected GameObject projectile;

    [SerializeField]
    protected Actor player;

    public override void performAction()
    {
		if(!gameObject.activeSelf)
			return;

        if (Time.time - lastActionTime > cooldown)
        {
            GameObject spawnedProjectile = Instantiate(projectile) as GameObject;
            spawnedProjectile.transform.position = projectile.transform.position;
            spawnedProjectile.GetComponent<Projectile>().Initialize(player.GetLastAnimationMovementDirection(), player);
            lastActionTime = Time.time;
        }


    }

    public override bool BlocksInputMovement()
    {
        return false;
    }

    public override bool IsBlockedBy(List<Action> activeActions)
    {
        return activeActions.Exists(x => x is Dash || x is Jump || x is Fall);
    }

    public override void ResetState()
    {
        throw new System.NotImplementedException();
    }

    public override bool IsActive()
    {
        return false;
    }


    public void PlayerChosen(int playerNumber)
    {
        Debug.Log("player chosen: " + playerNumber);
    }

}
