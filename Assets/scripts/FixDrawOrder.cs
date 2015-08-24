using UnityEngine;
using System.Collections;

public class FixDrawOrder : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        // fix draw order
        Vector3 currentPosition = transform.position;
        currentPosition.z = transform.position.y;
        transform.position = currentPosition;
    }
}
