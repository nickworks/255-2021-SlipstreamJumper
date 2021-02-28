using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kortge
{
    public class SpringBlock : OverlapObject
    {
        public override void OnOverlap(PlayerMovement pm)
        {
            pm.LaunchPlayer(new Vector3(0, 20, 0));
        }
    }
}