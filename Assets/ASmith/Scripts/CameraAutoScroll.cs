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
        public static bool isPickedUp = false;

        /// <summary>
        /// Variable that tracks how much longer the player has the bike pickup
        /// </summary>
        private float scrollTimer = 0;

        void Update()
        {
            print("scroll time: " + scrollTimer + ", scroll speed: " + scrollSpeed);
            if (scrollTimer > 0) // If the scroll timer is GREATER THAN 0...
            {
                scrollTimer -= Time.deltaTime; // Count down the timer
                if (scrollTimer <= 0) // If the scroll Timer is LESS THAN or EQUAL TO 0...
                {
                    isPickedUp = false; // Is picked up is set to false
                    transform.position += scrollSpeed * Time.deltaTime; // if !isPickedUp keep speed of CameraScroll at default speed
                }
            }

            if (scrollTimer > 0)
            {
                transform.position += scrollSpeed * Time.deltaTime * 2; // if BikePickup isPickedUp then double the speed of the CameraScroll
            }

            if (isPickedUp && scrollTimer <= 0)
            {
                scrollTimer = 5; // Sets the scroll timer
            } else transform.position += scrollSpeed * Time.deltaTime; // if !isPickedUp keep speed of CameraScroll at default speed

        }
    }
}

