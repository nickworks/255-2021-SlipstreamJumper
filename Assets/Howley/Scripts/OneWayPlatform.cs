using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Howley
{
    public class OneWayPlatform : OverlapObject
    {
        /// <summary>
        /// This integer keeps track of how many times the player has overlapped the platform.
        /// </summary>
        private int timesOverlapped = 0;

        public override void OnOverlap(PlayerMovement pm)
        { 
            //if (timesOverlapped == 0) timesOverlapped ++;
            //if (timesOverlapped >= 1) pm.ApplyFix(aabb.FindFix(aabb));
        }
    }
}

