using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Howley
{
    public class CameraAutoScroll : MonoBehaviour
    {
        /// <summary>
        /// How fast the camera will automatically move every second.
        /// </summary>
        public Vector3 scrollSpeed = new Vector3();

        void Start()
        {

        }

        void Update()
        {
            transform.position += scrollSpeed * Time.deltaTime;
        }
    }
}

