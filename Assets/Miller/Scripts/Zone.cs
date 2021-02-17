using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Miller
{
    public class Zone : SlipstreamJumper.Zone
    {

        new public static ZoneInfo info = new ZoneInfo()
        {
            zoneName = "Magma Ruins",
            creator = "Zion Miller",
            sceneFile = "ZoneMiller"
        };

        public AABB player;
        public AABB floor;

        void Start()
        {

        }

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
