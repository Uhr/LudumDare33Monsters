using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actor : MonoBehaviour
{
    public InputDevice input;
    public float movementSpeed = 1;
    public Collider2D collider;
    [SerializeField]
    public Action[] actions = new Action[4];
    public float hp = 50;

    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    SpriteAnimator walkingAnimator;
    [SerializeField]
    SpriteAnimator idleAnimator;
    enum AnimationState { CustomAction, Idle, Walking, None };
    AnimationState currentAnimationState = AnimationState.None;

    Vector2 lastAnimationMovementDirection = Vector2.zero;




    // Update is called once per frame
    void Update()
    {
        UpdateLastAnimationMovementDirection();

        ComputeMovement();




        // fix draw order
        Vector3 currentPosition = transform.position;
        currentPosition.z = transform.position.y;
        transform.position = currentPosition;

    }

    bool MovementChangesBlocked()
    {
        foreach (Action act in actions)
        {
            if (act.IsActive() && act.BlocksInputMovement())
            {

                return true;

            }
        }

        return false;
    }

    public Vector2 GetLastAnimationMovementDirection()
    {
        return lastAnimationMovementDirection;
    }

    void UpdateLastAnimationMovementDirection()
    {
        if (rigidBody.velocity != Vector2.zero)
        {
            lastAnimationMovementDirection = rigidBody.velocity;
        }
    }

    void ComputeMovement()
    {

        List<Action> activeActions = new List<Action>();

        foreach (Action a in actions)
        {
            if (a.IsActive())
            {
                activeActions.Add(a);
            }
        }

        for (int i = 0; i < actions.Length; i++)
        {
            if (input.GetButtonDown(i) && !actions[i].IsBlockedBy(activeActions))
            {
                actions[i].performAction();
                if(!activeActions.Contains(actions[i]))
                    activeActions.Add(actions[i]);
            }
        }

        bool dashInProgress = false;

        bool actionInProgress = false;
        foreach (Action act in actions)
        {
            if (act.IsActive())
            {
                actionInProgress = true;
                if (act is Dash)
                {
                    dashInProgress = true;
                }
            }
        }


        // choose animation if none in progress
        if (!actionInProgress)
        {
            bool idling = input.GetXAxis() == 0 && input.GetYAxis() == 0;

            if (idling)
            {

                if (currentAnimationState == AnimationState.Idle)
                {
                    idleAnimator.UpdateAnimation();
                }
                else
                {
                    currentAnimationState = AnimationState.Idle;
                    idleAnimator.StartAnimation();
                }

            }
            else
            {
                if (currentAnimationState == AnimationState.Walking)
                {
                    walkingAnimator.UpdateAnimation();
                }
                else
                {
                    currentAnimationState = AnimationState.Walking;
                    walkingAnimator.StartAnimation();
                }
            }

        }
        else
        {
            currentAnimationState = AnimationState.CustomAction;
        }



        if (!MovementChangesBlocked())
        {

            {
                rigidBody.velocity = Vector2.zero;

                Vector2 moveDir = new Vector2(input.GetXAxis(), input.GetYAxis()).normalized;
                moveDir *= movementSpeed;

                rigidBody.velocity += moveDir;
            }
        }
        else if (dashInProgress)
        {
            rigidBody.velocity = Vector2.zero;

            Vector2 moveDir = lastAnimationMovementDirection.normalized;
            moveDir *= movementSpeed * 2;

            rigidBody.velocity += moveDir;
        }


    }




}
