using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASmith
{
    public class CameraAutoScroll : MonoBehaviour
    {
        /// <summary>
        /// How much to add to position every second
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

