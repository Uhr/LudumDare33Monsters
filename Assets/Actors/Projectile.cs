using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour {
    [SerializeField]
    protected Rigidbody2D rigidBody;

    public abstract void Initialize(Vector2 playerDir);
}
