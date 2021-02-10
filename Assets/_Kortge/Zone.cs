using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kortge
{
    public class Zone : MonoBehaviour
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
        void Update()
        {
            if (player.OverlapCheck(floor))
            {
                print("overlapping...");
            }
        }
    }
}