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

        public AABB player;
        public AABB floor;

        void LateUpdate()
        {
            if (player.OverlapCheck(floor))
            {
                Vector3 fix = player.Findfix(floor);

                player.GetComponent<PlayerMovement>().ApplyFix(fix);

                //player.transform.position += fix;
                
                // Debug (Collision Check):
                print("Overlapping...");
            }
        }
    }
}
