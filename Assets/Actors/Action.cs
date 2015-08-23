using UnityEngine;
using System.Collections.Generic;

public abstract class Action : MonoBehaviour
{

    public abstract void performAction();
    public abstract bool BlocksInputMovement();
    public abstract bool IsBlockedBy(List<Action> activeActions);
    public abstract void ResetState();
    public abstract bool IsActive();

}
