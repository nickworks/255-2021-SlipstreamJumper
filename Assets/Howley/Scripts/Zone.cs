using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Howley
{
    public class Zone : SlipstreamJumper.Zone // Subclass of the whole project's zone class.
    {
        new static public ZoneInfo info = new ZoneInfo()
        {
            zoneName = "TBD",
            creator = "Erik Howley",
            sceneFile = "ZoneHowley"
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

