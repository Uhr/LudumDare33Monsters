using UnityEngine;
using System.Collections;


public class SimpleProjectile : Projectile {
    [SerializeField]
    private float speed;

    float creationTime;
    float lifeTime = 10;

    [SerializeField]
    SpriteAnimator animator;

    private Vector2 direction;

	// Use this for initialization
	override public void Initialize (Vector2 dir) {
        direction = dir.normalized;
        gameObject.SetActive(true);
        creationTime = Time.time;
        rigidBody.velocity = speed * direction;

        animator.StartAnimation();
        animator.UpdateAnimation();
	}


    void Update()
    {
        if (Time.time - creationTime > lifeTime)
        {
            Destroy(this.gameObject);
        }


    }

}
