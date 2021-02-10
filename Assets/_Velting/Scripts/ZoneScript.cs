using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Velting
{

    public class ZoneScript : SlipstreamJumper.Zone

    {

        new public static ZoneInfo info = new ZoneInfo()
        {
            zoneName = "",
            creator = "William Velting",
            sceneFile = "ZoneVelting"
        };

        public AABB player;
        public AABB floor;

        // Start is called before the first frame update
        void Start()
        {
        
        }

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
