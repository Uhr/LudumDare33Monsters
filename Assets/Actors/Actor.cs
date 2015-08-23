using UnityEngine;
using System.Collections;

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

        TriggerStateChanges();

        rigidBody.velocity = Vector2.zero;

        Vector2 moveDir = new Vector2(input.GetXAxis(), input.GetYAxis()).normalized;
        moveDir *= movementSpeed;

        rigidBody.velocity += moveDir;

        // fix draw order
        Vector3 currentPosition = transform.position;
        currentPosition.z = transform.position.y;
        transform.position = currentPosition;

    }

    public Vector2 getLastAnimationMovementDirection()
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

    void TriggerStateChanges()
    {



        if (input.GetButtonDown(0))
        {
            Debug.Log("pressed");
            actions[0].performAction();
        }


        bool actionInProgress = false;
        foreach (Action act in actions)
        {
            if (act.IsActive())
            {
                actionInProgress = true;
                break;
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



    }




}
