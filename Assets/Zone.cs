using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : SlipstreamJumper.Zone 
{

    new public static ZoneInfo info = new ZoneInfo()
    {
        zoneName = "Void",
        creator = "Nik Erickson",
        sceneFile = ""
    };

    public AABB player;
    public AABB floor;

    void LateUpdate()
    {
        if(player.OverlapCheck(floor))
        {
            print("overlapping...");
        }
    }
}
