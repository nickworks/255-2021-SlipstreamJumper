using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kortge
{
    public class Zone : SlipstreamJumper.Zone
    {

        new public static ZoneInfo info = new ZoneInfo()
        {
            zoneName = "",
            creator = "Tyler Kortge",
            sceneFile = ""
        };


        public AABB player;
        public AABB floor;

        // Update is called once per frame
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