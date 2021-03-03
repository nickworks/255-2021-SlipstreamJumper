using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Howley
{
    public class Zone : SlipstreamJumper.Zone // Subclass of the whole project's zone class.
    {
        /// <summary>
        /// Hold information on the master Zone script.
        /// </summary>
        new static public ZoneInfo info = new ZoneInfo()
        {
            zoneName = "High Sky Jumper",
            creator = "Erik Howley",
            sceneFile = "ZoneHowley"
        };

        // Singleton:
        public static Zone main;

        /// <summary>
        /// Hold reference to the AABB script.
        /// </summary>
        public AABB aabb;

        /// <summary>
        /// Create an array to hold all platforms in the level.
        /// </summary>
        public List<AABB> platforms = new List<AABB>();

        /// <summary>
        /// Create an array to hold all powerups in the level.
        /// </summary>
        public List<AABB> powerups = new List<AABB>();

        /// <summary>
        /// Create an array to hold all one way platforms in the level.
        /// </summary>
        public List<AABB> oneWay = new List<AABB>();

        /// <summary>
        /// Runs before the start function.
        /// </summary>
        void Awake()
        {
            if (main != null) // Already have.
            {
                Destroy(gameObject);
            } else
            {
                main = this;
            }
            
        }

        /// <summary>
        /// If the Zone is destroyed, it now is null.
        /// </summary>
        private void OnDestroy()
        {
            if (main == this) main = null;
        }

        void LateUpdate()
        {
            if (!aabb) return; // If the player is dead/doesn't exist

            PlayerMovement pm = aabb.GetComponent<PlayerMovement>();

            // Checking collision between player and all platforms
            foreach(AABB box in platforms)
            {
                if (aabb.OverlapCheck(box))
                {
                    // Move the player out of the platform
                    pm.ApplyFix(aabb.FindFix(box));
                }
            }

            // Checking collision between player and all overlap objects
            foreach(AABB power in powerups)
            {
                if (aabb.OverlapCheck(power))
                {
                    // Do something.
                    OverlapObject oo = power.GetComponent<OverlapObject>();
                    if (oo)
                    {
                        oo.OnOverlap(pm);
                    }
                }
            }

            foreach(AABB one in oneWay)
            {
                if (aabb.OverlapCheck(one))
                {
                    OneWayPlatform ow = one.GetComponent<OneWayPlatform>();
                    ow.timesOverlapped ++;
                    pm.ApplyFix(aabb.FindFix(one));
                }
            }
            
        }

        /// <summary>
        /// This function inserts platforms into the array.
        /// </summary>
        /// <param name="platform"></param>
        public void AddPlatform(AABB platform)
        {
            platforms.Add(platform);
        }

        /// <summary>
        /// This function removes platforms from the array.
        /// </summary>
        /// <param name="platform"></param>
        public void RemovePlatform(AABB platform)
        {
            platforms.Remove(platform);
        }
    }
}

