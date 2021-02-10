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

        public AABB player;
        public AABB floor;

        void LateUpdate() {

            if (player.OverlapCheck(floor)) {
                print("Collision");
                Vector3 fix = player.FindFix(floor);

                player.GetComponent<PlayerMovement>().ApplyFix(fix);

            }

        }
    }
}