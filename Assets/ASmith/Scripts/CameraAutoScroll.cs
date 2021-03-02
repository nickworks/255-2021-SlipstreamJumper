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

        /// <summary>
        /// Whether or not the BikePickup has been picked up
        /// </summary>
        public bool isPickedUp = false;

        void Update()
        {
            if (isPickedUp)
            {
                transform.position += scrollSpeed * Time.deltaTime * 2; // if BikePickup isPickedUp then double the speed of the CameraScroll
            } else transform.position += scrollSpeed * Time.deltaTime; // if !isPickedUp keep speed of CameraScroll at default speed
        }
    }
}

