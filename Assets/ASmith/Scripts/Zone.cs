using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASmith
{
    public class Zone : SlipstreamJumper.Zone
    {
        new public static ZoneInfo info = new ZoneInfo()
        {
            zoneName = "Toni Land",
            creator = "Antonio Smith",
            sceneFile = "ZoneASmith"
        };

        public AABB player;
        public AABB floor;

        void LateUpdate()
        {
            if (player.OverlapCheck(floor))
            {
                Vector3 fix = player.FindFix(floor);
                player.GetComponent<PlayerMovement>().ApplyFix(fix);
            }
        }
    }
}
