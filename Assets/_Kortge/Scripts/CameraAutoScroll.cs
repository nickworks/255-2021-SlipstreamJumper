using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kortge
{
    public class CameraAutoScroll : MonoBehaviour
    {
        /// <summary>
        /// How much to add to position every second. (m/s)
        /// </summary>
        public Vector3 scrollSpeed = new Vector3();

        // Update is called once per frame
        void Update()
        {
            transform.position += scrollSpeed * Time.deltaTime; // Camera moves with time.
        }
    }
}