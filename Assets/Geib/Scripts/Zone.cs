using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geib
{
    public class Zone : SlipstreamJumper.Zone
    {

        new public static ZoneInfo info = new ZoneInfo()
        {
            zoneName = "",
            creator = "Samuel Geib",
            sceneFile = "ZoneGeib",

        };


        // singleton:

        public static Zone main;

        public AABB player;

        // List of all platforms that the player can collide with
        private List<AABB> platforms = new List<AABB>();

        public List<AABB> powerups = new List<AABB>();

        private void Awake()
        {
            if(main != null) // Singleton already exists...
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
                    pm.ApplyFix(player.Findfix(box));

                }
            }

            foreach(AABB power in powerups)
            {
                if (player.OverlapCheck(power))
                {
                    SpringBlock sb = power.GetComponent<SpringBlock>();
                    if(sb)
                    {
                        sb.PlayerHit(pm);
                    }
                }
            }

          
        }
        /// <summary>
        /// This function adds platform to our big list of platforms.
        /// </summary>
        /// <param name="platform"></param>
        public void AddPlatform(AABB platform)
        {
            platforms.Add(platform);
        }
        /// <summary>
        /// This function removes a platform from our big list of platforms.
        /// </summary>
        /// <param name="platform"></param>
        public void RemovePlatform(AABB platform)
        {
            platforms.Remove(platform);
        }
    }
}
