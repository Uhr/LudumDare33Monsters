using UnityEngine;
using System.Collections.Generic;

public class Dash : Action, PlayerChoiceReactor
{

    bool isActing = false;
    [SerializeField]
    float duration;
    private float startTime;

    [SerializeField]
    SpriteAnimator animator1;
    [SerializeField]
    SpriteAnimator animator2;
    SpriteAnimator chosenAnimator;

    override public void performAction()
    {

        if (!isActing)
        {
            isActing = true;
            startTime = Time.time;
            chosenAnimator.StartAnimation();
        }

    }

    override public bool BlocksInputMovement()
    {
        return true;
    }

    override public bool IsBlockedBy(List<Action> activeActions)
    {
		return activeActions.Exists(x => x is Jump || x is Fall);
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
            chosenAnimator.UpdateAnimation();
        }
    }



    public override bool IsActive()
    {
        return isActing;
    }

    public void PlayerChosen(int playerNumber)
    {
        if (playerNumber == 1 || playerNumber == 3)
        {
            chosenAnimator = animator1;
        }
        else
        {
            chosenAnimator = animator2;
        }
    }
}