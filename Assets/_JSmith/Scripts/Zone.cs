using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JSmith
{
    public class Zone : SlipstreamJumper.Zone
    {
        new public static ZoneInfo info = new ZoneInfo()
        {
            zoneName = "Level Name",
            creator = "Jordan",
            sceneFile = "ZoneJSmith"
        };

        public AABB player;
        public AABB floor;

        void LateUpdate()
        {
            if (player.OverlapCheck(floor))
            {
               Vector3 fix = player.FindFix(floor);
            }
        }
    }
}
