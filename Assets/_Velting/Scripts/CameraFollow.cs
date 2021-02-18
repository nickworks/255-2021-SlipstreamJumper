using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Velting
{

    public class CameraFollow : MonoBehaviour
    {

        public new Transform target;
        void Start()
        {

        }

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
}