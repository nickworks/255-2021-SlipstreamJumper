using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hodgkins
{

    public class Zone : SlipstreamJumper.Zone
    {
        /// <summary>
        /// The name and information of my zone
        /// </summary>
        new public static ZoneInfo info = new ZoneInfo()
        {
            zoneName = "Tom's Green Zone",
            creator = "Tom Hodgkins",
            sceneFile = "ZoneHodgkins"
        };

        // singleton:
        public static Zone main;

        public AABB player;
        public AABB floor;

        // one variable to hold all the platforms
        private List<AABB> platforms = new List<AABB>();

        public List<AABB> powerups = new List<AABB>();

        /// <summary>
        /// When my level is loaded...
        /// </summary>
        private void Awake()
        {
            if(main != null) // singleton already exists...
            {
                Destroy(gameObject);
            } else
            {
                main = this;
            }
        }
        
        /// <summary>
        /// When my level is done playing
        /// </summary>
        private void OnDestroy()
        {
            if (main == this) main = null;
        }

        /// <summary>
        /// Update function that's forced to update after everything else
        /// </summary>
        void LateUpdate()
        {
            if (!player) return; // no player, don't do collision
            
            PlayerMovement pm = player.GetComponent<PlayerMovement>();

            foreach(AABB box in platforms) //checking collision between PLAYER and all PLATFORMS
            {
                if(player.OverlapCheck(box))
                {
                    pm.ApplyFix(player.FindFix(box));
                }
            }        
            
            // checking collision between PLAYER and all OVERLAP-OBJECTS
            foreach(AABB power in powerups)
            {
                if (player.OverlapCheck(power))
                {
                    //player collides with powerup!
                    // do something...

                    OverlapObject oo = power.GetComponent<OverlapObject>();

                    if (oo)
                    {
                        oo.OnOverlap(pm);
                    }

                }
            }
        }

        /// <summary>
        /// This function adds a platform to the big list of platforms
        /// </summary>
        /// <param name="platform"></param>
        public void AddPlatform(AABB platform)
        {
            platforms.Add(platform);
        }

        /// <summary>
        /// Thjis functions removes platforms from the big list of platforms.
        /// </summary>
        /// <param name="platform"></param>
        public void RemovePlatform(AABB platform)
        {
            platforms.Remove(platform);
        }

    }
}