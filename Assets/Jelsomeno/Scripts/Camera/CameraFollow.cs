using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jelsomeno
{

    public class CameraFollow : MonoBehaviour
    {
        /// <summary>
        /// What the camera is following
        /// </summary>
        public Transform target;

        void Start()
        {

        }

        // Update is called once per frame
        void LateUpdate()
        {
            Vector3 pos = transform.position;

            pos.x = target.position.x + 5;
            pos.y = target.position.y;

            //transform.position = pos;

            //easing
            transform.position += (pos - transform.position) * Time.deltaTime * 10;

        }
    }
}
