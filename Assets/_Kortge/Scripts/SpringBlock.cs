using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kortge
{
    public class SpringBlock : OverlapObject // Shoots the player into the air upon collision.
    {
        public override void OnOverlap(PlayerMovement pm)
        {
            pm.LaunchPlayer(new Vector3(0, 20, 0));
        }
    }
}