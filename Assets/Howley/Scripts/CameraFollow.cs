using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Howley
{
    public class CameraFollow : MonoBehaviour
    {
        /// <summary>
        /// The thing the camera is following.
        /// </summary>
        public Transform target;

        void LateUpdate()
        {
            if (target)
            {
                Vector2 pos = new Vector2(transform.position.x, transform.position.y);
                Vector2 targetPos = new Vector2(target.position.x, target.position.y);
                pos = AnimMath.Slide(pos, target.position, .01f);
            }
            
        }
    }
}

