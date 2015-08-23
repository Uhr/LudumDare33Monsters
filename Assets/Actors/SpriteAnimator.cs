using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D playerRigidBody;
    [SerializeField]
    Actor actor;

    [SerializeField]
    SpriteRenderer targetSpriteRenderer;

    enum PossibleDirections { One, Four, Eight };
    [SerializeField]
    PossibleDirections numberOfDirections = PossibleDirections.One;

    [SerializeField]
    List<Sprite> spritesFor0;
    [SerializeField]
    List<Sprite> spritesFor45;
    [SerializeField]
    List<Sprite> spritesFor90;
    [SerializeField]
    List<Sprite> spritesFor135;
    [SerializeField]
    List<Sprite> spritesFor180;
    [SerializeField]
    List<Sprite> spritesFor225;
    [SerializeField]
    List<Sprite> spritesFor270;
    [SerializeField]
    List<Sprite> spritesFor315;

    [SerializeField]
    bool cycleAnimation;
    [SerializeField]
    float animationDuration;


    float animationStartTime;




    public void StartAnimation()
    {
        animationStartTime = Time.time;
    }



    // Update is called once per frame
    public void UpdateAnimation()
    {
        // choose direction
        Vector2 playerVelocity = actor.getLastAnimationMovementDirection();
        float angle = Vector2.Angle(Vector2.up, playerVelocity);
        if (playerVelocity.x < 0)
        {
            angle = 360 - angle;
        }
        List<Sprite> chosenSprites;

        if (numberOfDirections == PossibleDirections.One)
        {
            chosenSprites = spritesFor0;
        }
        else if (numberOfDirections == PossibleDirections.Four)
        {
            if (angle < 45f || angle > 315f)
            {
                chosenSprites = spritesFor0;
            }
            else if (angle < 135f)
            {
                chosenSprites = spritesFor90;
            }
            else if (angle < 225f)
            {
                chosenSprites = spritesFor180;
            }
            else
            {
                chosenSprites = spritesFor270;
            }

        }
        else
        {
            if (angle < 22.5f || angle > 337.5)
            {
                chosenSprites = spritesFor0;
            }
            else if (angle < 67.5f)
            {
                chosenSprites = spritesFor45;
            }
            else if (angle < 112.5f)
            {
                chosenSprites = spritesFor90;
            }
            else if (angle < 157.5f)
            {
                chosenSprites = spritesFor135;
            }
            else if (angle < 202.5f)
            {
                chosenSprites = spritesFor180;
            }
            else if (angle < 247.5f)
            {
                chosenSprites = spritesFor225;
            }
            else if (angle < 292.5f)
            {
                chosenSprites = spritesFor270;
            }
            else
            {
                chosenSprites = spritesFor315;
            }
        }

        Debug.Log(angle);




        // timing
        if (cycleAnimation)
        {
            float passedTime = Time.time - animationStartTime;
            if (passedTime > animationDuration)
            {
                float overShoot = passedTime - animationDuration;
                animationStartTime = Time.time - overShoot;
            }
        }


        float passedTimeRatio = (Time.time - animationStartTime) / animationDuration;
        int index = Mathf.FloorToInt(passedTimeRatio * chosenSprites.Count);
        if (index >= chosenSprites.Count)
        {
            index = chosenSprites.Count - 1;
        }

        Sprite chosenSprite = chosenSprites[index];

        targetSpriteRenderer.sprite = chosenSprite;

    }

    List<Sprite> GetSpritesByIndex(int index)
    {
        switch (index)
        {
            case 1: return spritesFor45;
            case 2: return spritesFor90;
            case 3: return spritesFor135;
            case 4: return spritesFor180;
            case 5: return spritesFor225;
            case 6: return spritesFor270;
            case 7: return spritesFor315;
            default: return spritesFor0;
        }
    }


}
