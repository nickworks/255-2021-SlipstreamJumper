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

        public AABB player;

        private List<AABB> platforms = new List<AABB>();

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
            PlayerMovement pm = player.GetComponent<PlayerMovement>();

            foreach(AABB box in platforms)
            {
                if (player.OverlapCheck(box))
                { 
                    pm.ApplyFix(player.FindFix(box));
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

