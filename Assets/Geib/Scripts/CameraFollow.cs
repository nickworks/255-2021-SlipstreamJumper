using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geib
{
    public class CameraFollow : MonoBehaviour
    {
        /// <summary>
        /// This is the object the camera is following.
        /// This should be the palyer.
        /// </summary>
        public Transform target;
        // Start is called before the first frame update
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

            // asympottic easing:
            // exponential slide:
            transform.position += (pos - transform.position) * Time.deltaTime * 10;

        }
    }
}