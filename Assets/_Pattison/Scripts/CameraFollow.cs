using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    /// <summary>
    /// The thing that the camera is following.
    /// </summary>
    public Transform target;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = transform.position;
        
        pos.x = target.position.x + 5;
        pos.y = target.position.y;

        //transform.position = pos;

        // asymptotic easing:
        // exponential slide:

        transform.position += (pos - transform.position) * Time.deltaTime * 10;

    }
}
