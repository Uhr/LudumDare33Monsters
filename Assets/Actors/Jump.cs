using UnityEngine;
using System.Collections.Generic;

public class Jump : Action
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
    SpriteAnimator animator;

    override public void performAction()
    {

        Debug.Log("Jump started");
        if (!isJumping)
        {
            isJumping = true;
            startTime = Time.time;
            animator.StartAnimation();
        }

    }

    override public bool BlocksInputMovement()
    {
        return false;
    }

    override public bool IsBlockedBy(List<Action> activeActions)
    {
        return activeActions.Exists(x => x is Dash);
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
            float animationHeight = animation.Evaluate(passedTimeRatio) * maxHeight;
            playerSprite.localPosition = new Vector3(playerSprite.localPosition.x, animationHeight, playerSprite.localPosition.z);

            if (passedTimeRatio >= 1)
            {
                isJumping = false;
            }

            animator.UpdateAnimation();
        }
    }



    public override bool IsActive()
    {
        return isJumping;
    }
}
