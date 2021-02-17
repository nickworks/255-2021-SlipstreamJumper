using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASmith
{
    public class CameraFollow : MonoBehaviour
    {
        /// <summary>
        /// The thing the camera is following
        /// </summary>
        public Transform target;
        void Start()
        {

        }

        void Update()
        {
            Vector3 pos = transform.position;

            pos.x = target.position.x;
            pos.y = target.position.y;

            // transform.position = pos;

            // easing:
            transform.position += (pos - transform.position) * Time.deltaTime * 10;
        }
    }
}

