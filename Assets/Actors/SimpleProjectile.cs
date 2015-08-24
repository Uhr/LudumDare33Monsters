using UnityEngine;
using System.Collections;


public class SimpleProjectile : Projectile, PlayerChoiceReactor
{
    [SerializeField]
    private float speed;

    float creationTime;
    float lifeTime = 10;

    [SerializeField]
    SpriteAnimator animator1;
    [SerializeField]
    SpriteAnimator animator2;

    SpriteAnimator chosenAnimator;

    private Vector2 direction;

    [SerializeField]
    Transform projectileSprite;


    // Use this for initialization
    override public void Initialize(Vector2 dir, Actor owner)
    {
        direction = dir.normalized;
        gameObject.SetActive(true);
        creationTime = Time.time;
        rigidBody.velocity = speed * direction;

        this.owner = owner;

        if (owner.GetChosenPlayerNumber() == 1 || owner.GetChosenPlayerNumber() == 3)
        {
            chosenAnimator = animator1;
        }
        else
        {
            chosenAnimator = animator2;
        }

        chosenAnimator.StartAnimation();
        chosenAnimator.UpdateAnimation();
    }


    void Update()
    {
        if (Time.time - creationTime > lifeTime)
        {
            DestroySelf();
        }
    }

    void DestroySelf()
    {
        Destroy(this.gameObject);
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        Actor actor = collider.gameObject.GetComponent<Actor>();
        if (actor != null)
        {
            if (actor != this.owner)
            {
                DestroySelf();
            }
        }

    }


    public void PlayerChosen(int playerNumber)
    {
        float standardYOffset;
        if (playerNumber == 1 || playerNumber == 3)
        {
            standardYOffset = 0;
        }
        else
        {
            standardYOffset = -0.25f;
        }

        projectileSprite.localPosition += new Vector3(0, standardYOffset, 0);


        this.gameObject.SetActive(false);
    }

}
