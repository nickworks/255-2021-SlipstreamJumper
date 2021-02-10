using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hopkins {
    public class Zone : SlipstreamJumper.Zone
    {
        new public static ZoneInfo info = new ZoneInfo
        {
            zoneName = "Zone Name",
            creator = "Griffen H",
            sceneFile = "defaultscene"

        };

        public AABB player;
        public AABB floor;

        // Update is called once per frame
        void Update()
        {
            if (player.OverlapCheck(floor))
            {
                Vector3 fix = player.FindFix(floor);
                player.GetComponent<PlayerMovement>().ApplyFix(fix);
                //player.transform.position += fix;
            }
        }
    }
}
