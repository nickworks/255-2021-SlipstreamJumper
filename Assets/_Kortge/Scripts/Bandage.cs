using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kortge
{
    [RequireComponent(typeof(AABB))]
    public class Bandage : OverlapObject // Gives the player a bandage on overlap with this object.
    {
        public override void OnOverlap(PlayerMovement pm)
        {
            pm.AddBandage();
            Destroy(gameObject);
        }
    }
}