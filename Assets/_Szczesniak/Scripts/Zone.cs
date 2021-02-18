using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak {
    public class Zone : SlipstreamJumper.Zone {


        new public static ZoneInfo info = new ZoneInfo() {
            zoneName = "Name",
            creator = "Gavin Szczesniak",
            sceneFile = "Zone_Szczesniak"
        };

        // Singleton
        public static Zone main;



        public AABB player;

        // one variable to hold all of the platforms:
        private List<AABB> platforms = new List<AABB>();

        public List<AABB> powerups = new List<AABB>();

        private void Awake() {
            if (main != null) { // singleton already exist...
                Destroy(gameObject);
            } else {
                main = this;
            }
        }

        private void OnDestroy() {
            if (main == this) main = null;
        }

        void LateUpdate() {

            PlayerMovement pm = player.GetComponent<PlayerMovement>();

            foreach(AABB box in platforms) {
                if (player.OverlapCheck(box)) {
                    pm.ApplyFix(player.FindFix(box));
                }
            }

            foreach(AABB power in powerups)
            {
                if (player.OverlapCheck(power)) {
                    // player collides with powerup!
                    // do something...

                    SpringBlock sb = power.GetComponent<SpringBlock>();
                    if (sb) {
                        sb.PlayerHit(pm);
                    }
                }
            }
           
            /*
            if (player.OverlapCheck(floor)) {
                print("Collision");
                Vector3 fix = player.FindFix(floor);

                player.GetComponent<PlayerMovement>().ApplyFix(fix);

            }
            */

        }

        /// <summary>
        /// This function add a platform to our big list of platforms
        /// </summary>
        /// <param name="platform"></param>
        public void AddPlatform(AABB platform)
        {
            platforms.Add(platform);
        }

        /// <summary>
        /// This function removes a platform to our big list of platforms
        /// </summary>
        /// <param name="platform"></param>
        public void RemovePlatform(AABB platform)
        {
            platforms.Remove(platform);
        }
    }
}