using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour
{

    protected Actor owner;

    [SerializeField]
    protected Rigidbody2D rigidBody;

    public abstract void Initialize(Vector2 playerDir, Actor owner);

    public Actor GetOwner()
    {
        return owner;
    }


}
