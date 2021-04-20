using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASmith
{
    public class Zone : SlipstreamJumper.Zone
    {
        new public static ZoneInfo info = new ZoneInfo()
        {
            zoneName = "Toni's Pizza Land", // The name of my level in the project
            creator = "Antonio Smith", // My name, i.e. the creator of this level
            sceneFile = "ZoneASmith"
        };

        public AABB player;
        // singleton:
        public static Zone main;

        // variable to hold all platforms
        private List<AABB> platforms = new List<AABB>();
        // variable to hold all powerups
        public List<AABB> powerups = new List<AABB>();

        private void Awake() // awake runs before start functions
        {
            if (main != null)
            {
                Destroy(gameObject);
            }
            else
            {
                main = this;
            }
        }

        private void OnDestroy()
        {
            if (main == this) main = null;
        }

        // Checks collision between PLAYER and all PLATFORMS
        void LateUpdate()
        {
            if (!player) return; // if player dead, dont do collision detection

            PlayerMovement pm = player.GetComponent<PlayerMovement>();

            // prevents player from overlapping with platforms in list
            foreach (AABB box in platforms)
            {
                if (player.OverlapCheck(box))
                { 
                    pm.ApplyFix(player.FindFix(box));
                }
            }

            // Checks collision between PLAYER and all OVERLAP OBJECTS
            foreach (AABB power in powerups)
            {
                if (player.OverlapCheck(power))
                {
                    // player collides with powerup
                    OverlapObject oo = power.GetComponent<OverlapObject>();
                    if (oo)
                    {
                        oo.OnOverlap(pm);
                    }
                }
            }
        }

        /// <summary>
        /// This function adds a platform to the list of platforms
        /// </summary>
        /// <param name="platform"></param>
        public void AddPlatform(AABB platform)
        {
            platforms.Add(platform);
        }

        /// <summary>
        /// This function removes a platform from the list of platforms
        /// </summary>
        /// <param name="platform"></param>
        public void RemovePlatform(AABB platform)
        {
            platforms.Remove(platform);
        }

    } 
}
