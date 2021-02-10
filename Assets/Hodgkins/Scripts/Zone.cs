using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hodgkins
{

    public class Zone : SlipstreamJumper.Zone
    {

        new public static ZoneInfo info = new ZoneInfo()
        {
            zoneName = "Tom's Zone",
            creator = "Tom Hodgkins",
            sceneFile = "ZoneHodgkins"
        };


        public AABB player;
        public AABB floor;
        
        
        void Update()
        {
            if (player.OverlapCheck(floor))
            {
                print("overlapping...");
            }
        }
    }
}