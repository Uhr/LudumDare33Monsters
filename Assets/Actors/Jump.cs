using UnityEngine;
using System.Collections.Generic;

public class Jump : Action, PlayerChoiceReactor
{

    bool isJumping = false;
    [SerializeField]
    float duration;
    [SerializeField]
    float maxHeight = 1;
    [SerializeField]
    AnimationCurve animation;
    [SerializeField]
    Transform playerSprite;
    private float startTime;

    [SerializeField]
    SpriteAnimator animator1;
    [SerializeField]
    SpriteAnimator animator2;
    SpriteAnimator chosenAnimator;


    float standardYOffset = 0;

    override public void performAction()
	{
        if (!isJumping)
        {
            isJumping = true;
            startTime = Time.time;
            chosenAnimator.StartAnimation();
        }

    }

    override public bool BlocksInputMovement()
    {
        return false;
    }

    override public bool IsBlockedBy(List<Action> activeActions)
    {
        return activeActions.Exists(x => x is Dash || x is Fall);
    }


    override public void ResetState()
    {
        isJumping = false;
    }

    void Update()
    {
        if (isJumping)
        {

            float passedTimeRatio = (Time.time - startTime) / duration;
            float animationHeight = animation.Evaluate(passedTimeRatio) * maxHeight + standardYOffset;
            playerSprite.localPosition = new Vector3(playerSprite.localPosition.x, animationHeight, playerSprite.localPosition.z);

            if (passedTimeRatio >= 1)
            {
                isJumping = false;
            }

            chosenAnimator.UpdateAnimation();
        }
    }



    public override bool IsActive()
    {
        return isJumping;
    }

    public void PlayerChosen(int playerNumber)
    {
        if (playerNumber == 1 || playerNumber == 3)
        {
            standardYOffset = 0;
            chosenAnimator = animator1;
        }
        else
        {
            standardYOffset = -0.085f;
            chosenAnimator = animator2;
        }

        playerSprite.localPosition += new Vector3(0, standardYOffset, 0);

    }
}
