using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    /// <summary>
    /// This is the thing the camera is following.
    /// </summary>
    public Transform target;


    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = transform.position;


        pos.x = target.position.x;
        pos.y = target.position.y;

        //transform.position = pos;
        //asymptotic easing:
        //exponential slide:
        transform.position += (pos - transform.position) * Time.deltaTime * 10;
    }
}
