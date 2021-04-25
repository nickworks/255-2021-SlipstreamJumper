using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelsomeno
{

    public class OneWayPlatform : OverlapObject
    {
        private int Overlapping = 0;

        public override void OnOverlap(PlayerMovement pm)
        {
            //if (Overlapping == 0) Overlapping++;
            //if (Overlapping >= 1) pm.ApplyFix(aabb.FindFix(aabb));
        }


    }
}
