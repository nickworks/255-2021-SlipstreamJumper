using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kortge
{
    [RequireComponent(typeof(AABB))]
    public class HazardBlock : OverlapObject
    {
        public override void OnOverlap(PlayerMovement pm)
        {
            pm.KillPlayer();
        }
    }
}