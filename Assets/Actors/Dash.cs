using UnityEngine;
using System.Collections.Generic;

public class Dash : Action
{

    bool isActing = false;
    [SerializeField]
    float duration;
    private float startTime;

    [SerializeField]
    SpriteAnimator animator;

    override public void performAction()
    {

        if (!isActing)
        {
            isActing = true;
            startTime = Time.time;
            animator.StartAnimation();
        }

    }

    override public bool BlocksInputMovement()
    {
        return true;
    }

    override public bool IsBlockedBy(List<Action> activeActions)
    {
        //return activeActions.Exists(x => x is Jump);
        foreach (Action a in activeActions)
        {
            if (a is Jump)
            {
                return true;
            }
        }

        return false;
    }


    override public void ResetState()
    {
        isActing = false;
    }

    void Update()
    {
        if (isActing)
        {
            if (Time.time - startTime > duration)
            {
                isActing = false;
            }
            animator.UpdateAnimation();
        }
    }



    public override bool IsActive()
    {
        return isActing;
    }
}