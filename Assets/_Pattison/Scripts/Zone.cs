using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pattison {
    public class Zone : SlipstreamJumper.Zone {

        new public static ZoneInfo info = new ZoneInfo() {
            zoneName = "Magma Ruins",
            creator = "Nick Pattison",
            sceneFile = "ZonePattison"
        };

        // singleton:
        public static Zone main;

        public AABB player;

        // one variable to hold all of the platforms:
        private List<AABB> platforms = new List<AABB>();

        public List<AABB> powerups = new List<AABB>();

        private void Awake() {
            if(main != null) { // singleton already exists...
                Destroy(gameObject);
            } else {
                main = this;
            }
        }
        private void OnDestroy() {
            if(main == this) main = null;
        }

        void LateUpdate() {

            if (!player) return; // no player, don't do collision detection

            PlayerMovement pm = player.GetComponent<PlayerMovement>();

            // checking collision between PLAYER and all PLATFORMS:
            foreach(AABB box in platforms) {
                if (player.OverlapCheck(box)) {
                    pm.ApplyFix(player.FindFix(box));
                }
            }

            // checking collision between PLAYER and all OVERLAP-OBJECTS
            foreach(AABB power in powerups) {
                if (player.OverlapCheck(power)) {
                    // player collides with powerup!
                    // do something....

                    OverlapObject oo = power.GetComponent<OverlapObject>();

                    if (oo) {
                        oo.OnOverlap(pm);
                    }
                }
            }

        }
        /// <summary>
        /// This function adds a platform to our big list of platforms.
        /// </summary>
        /// <param name="platform"></param>
        public void AddPlatform(AABB platform) {
            platforms.Add(platform);
        }
        /// <summary>
        /// This function removes a platform from our big list of platforms.
        /// </summary>
        /// <param name="platform"></param>
        public void RemovePlatform(AABB platform) {
            platforms.Remove(platform);
        }

    }
}