using UnityEngine;
using System.Collections;

public abstract class Action : MonoBehaviour
{

    public abstract void performAction();
    public abstract bool BlocksInputMovement();
    public abstract bool IsBlockedBy(Action otherAction);
    public abstract void ResetState();
    public abstract bool IsActive();

}
