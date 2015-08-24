using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MakeAllBox2dCollidersTriggers : MonoBehaviour
{

    bool done = false;


    // Use this for initialization
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        if (!done)
        {

            BoxCollider2D[] colliders2D = GetComponents<BoxCollider2D>();

            foreach (BoxCollider2D colliderOLD in colliders2D)
            {
                colliderOLD.isTrigger = true;
            }



            done = true;
        }
    }
}