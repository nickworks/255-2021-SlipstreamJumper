using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Howley
{
    public class OneWayPlatform : MonoBehaviour
    {
        /// <summary>
        /// Hold reference to the AABB script.
        /// </summary>
        AABB aabb;

        /// <summary>
        /// Hold reference to the PlayerMovement script.
        /// </summary>
        PlayerMovement pm;

        /// <summary>
        /// This variable is a cooldown for letting the player pass through a platform.
        /// </summary>
        public float overlapCooldown = 2;

        /// <summary>
        /// This variable keeps track of how many times the player overlaps a platform.
        /// </summary>
        public int timesOverlapped = 0;

        private void Start()
        {
            aabb = GetComponent<AABB>();
            pm = GetComponent<PlayerMovement>();

            Zone.main.oneWay.Add(aabb);
            
        }

        void Update()
        {
            if (timesOverlapped >= 1) 
            {
                RunCooldown(); 
            }

            if (overlapCooldown <= 0 && timesOverlapped >= 1)
            {
                overlapCooldown = 2;
            }
        }

        /// <summary>
        /// This function removes the platform from the arraylist in the Zone script.
        /// </summary>
        private void OnDestroy()
        {
            if (Zone.main == null) return;
            Zone.main.oneWay.Remove(aabb);
        }

        /// <summary>
        /// This function runs the cooldown if the player overlaps a platform.
        /// </summary>
        private void RunCooldown()
        {
            overlapCooldown -= Time.deltaTime;
        }
    }
}

