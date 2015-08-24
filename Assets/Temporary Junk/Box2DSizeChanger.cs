using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Box2DSizeChanger : MonoBehaviour
{

    [SerializeField]
    float sizeChange = 0;
    [SerializeField]
    bool isDone = false;

    // Use this for initialization
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        if (!isDone && sizeChange != 0)
        {

            BoxCollider2D[] colliders2D = GetComponents<BoxCollider2D>();

            foreach (BoxCollider2D colliderOLD in colliders2D)
            {
                colliderOLD.size += new Vector2(sizeChange, sizeChange);
            }
            
            isDone = true;
        }
    }


}