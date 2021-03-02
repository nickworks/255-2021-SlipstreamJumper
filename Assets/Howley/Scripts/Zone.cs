using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Howley
{
    public class Zone : SlipstreamJumper.Zone // Subclass of the whole project's zone class.
    {
        new static public ZoneInfo info = new ZoneInfo()
        {
            zoneName = "TBD",
            creator = "Erik Howley",
            sceneFile = "ZoneHowley"
        };

        // Singleton:
        public static Zone main;

        public AABB aabb;

        private List<AABB> platforms = new List<AABB>();

        public List<AABB> powerups = new List<AABB>();

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
            
        }

        /// <summary>
        /// This function inserts platforms into the array.
        /// </summary>
        /// <param name="platform"></param>
        public void AddPlatform(AABB platform)
        {
            platforms.Add(platform);
        }

        public void RemovePlatform(AABB platform)
        {
            platforms.Remove(platform);
        }
    }
}

