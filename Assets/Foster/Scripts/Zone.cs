using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Foster
{
    public class Zone : SlipstreamJumper.Zone
    {
        new public static ZoneInfo info = new ZoneInfo()
        {

            zoneName = "",
            creator = "Emily Foster",
            sceneFile = "ZoneFoster"

        };



        public AABB player;
        public AABB floor;

        
        void Update()
        {
            if (player.OverlapCheck(floor))
            {
                print("overlapping");
            }
        }
    }
}