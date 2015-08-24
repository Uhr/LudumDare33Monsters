using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Replace3dWith2dCollider : MonoBehaviour
{

    public GameObject targetObject;
    bool done = false;


    // Use this for initialization
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        if (!done && targetObject != null)
        {

            BoxCollider[] colliders3D = GetComponents<BoxCollider>();

            foreach (BoxCollider colliderOLD in colliders3D)
            {

                BoxCollider2D newCollider = targetObject.AddComponent<BoxCollider2D>();
                if (newCollider == null)
                {
                    Debug.Log("nothing");
                }
                newCollider.offset = colliderOLD.center;
                newCollider.size = colliderOLD.size;
            }



            done = true;
        }
    }
}
