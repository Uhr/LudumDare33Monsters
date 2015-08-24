using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actor : MonoBehaviour
{
    public InputDevice input;
    public float movementSpeed = 1;
    [SerializeField]
    public Action[] actions = new Action[4];
    public float hp = 50;

    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    private float invincibleDuration = 2;

	[SerializeField]
	private float respawnMoveBlockDuration = 0.5f;

    [SerializeField]
    private ProjectileAttack attack;

    [SerializeField]
    SpriteAnimator walkingAnimator1;
    [SerializeField]
    SpriteAnimator walkingAnimator2;
    SpriteAnimator chosenWalkingAnimator;
    [SerializeField]
    SpriteAnimator idleAnimator1;
    [SerializeField]
    SpriteAnimator idleAnimator2;
    SpriteAnimator chosenIdleAnimator;
    enum AnimationState { CustomAction, Idle, Walking, None };
    AnimationState currentAnimationState = AnimationState.None;

    Vector2 lastAnimationMovementDirection = Vector2.zero;

    [SerializeField]
    int chosenPlayerNumber;

    [SerializeField]
    SpriteRenderer playerSprite;

    GameController gameController;

    private float invincibleStart = 0;

    private bool isInvincible = false;


    public void Initialize(int playerNumber, InputDevice input, GameController gameController)
    {
        this.gameController = gameController;

        chosenPlayerNumber = playerNumber;
        BroadcastMessage("PlayerChosen", chosenPlayerNumber);
        this.input = input;

        // chose animators
        if (playerNumber == 1 || playerNumber == 3)
        {
            chosenIdleAnimator = idleAnimator1;
            chosenWalkingAnimator = walkingAnimator1;
        }
        else
        {
            chosenIdleAnimator = idleAnimator2;
            chosenWalkingAnimator = walkingAnimator2;
        }


    }

    bool shouldBeInvincible()
    {
        return Time.time - invincibleStart <= invincibleDuration;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLastAnimationMovementDirection();

        ComputeMovement();


        if (isInvincible)
        {
            if (!shouldBeInvincible())
                SetInvincible(false);
            else
            {
                Color c = playerSprite.color;
				if(isRespawnMovementBlockActive())
					playerSprite.color = new Color(c.r, c.g, c.b, 0.5f);
				else
                	playerSprite.color = new Color(c.r, c.g, c.b, 2 * Time.time % 1f > 0.5f ? 0.5f : 1f);
            }
        }
    }

	private bool isRespawnMovementBlockActive()
	{
		return Time.time - invincibleStart < respawnMoveBlockDuration;
	}

    public int GetChosenPlayerNumber()
    {
        return chosenPlayerNumber;
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

		if(isRespawnMovementBlockActive())
		{
			rigidBody.velocity = Vector2.zero;
			return true;
		}

		return false;
    }

    public virtual Vector2 GetLastAnimationMovementDirection()
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
                if (!activeActions.Contains(actions[i]))
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
                    chosenIdleAnimator.UpdateAnimation();
                }
                else
                {
                    currentAnimationState = AnimationState.Idle;
                    chosenIdleAnimator.StartAnimation();
                }

            }
            else
            {
                if (currentAnimationState == AnimationState.Walking)
                {
                    chosenWalkingAnimator.UpdateAnimation();
                }
                else
                {
                    currentAnimationState = AnimationState.Walking;
                    chosenWalkingAnimator.StartAnimation();
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

    void OnTriggerStay2D(Collider2D collider)
    {
        Projectile projectile = collider.gameObject.GetComponent<Projectile>();
        bool jumping = GetComponentInChildren<Jump>().IsActive();
        if (projectile != null)
        {
            // die
            if (projectile.GetOwner() != this && !isInvincible && !(jumping && projectile.isJumpable))
            {
                gameController.PlayerKilled(this, projectile.GetOwner());
                Destroy(projectile);
            }
        }
        else if (collider.gameObject.CompareTag("Ground"))
        {
            if (!jumping)
            {
				gameController.PlayerKilled(this, null);
            }
        }
    }


    public void SetInvincible(bool invincible)
    {
        if (invincible)
            invincibleStart = Time.time;

        isInvincible = invincible;

        attack.gameObject.SetActive(!invincible);
        Color c = playerSprite.color;
        playerSprite.color = new Color(c.r, c.g, c.b, invincible ? 0.5f : 1f);
    }
}
