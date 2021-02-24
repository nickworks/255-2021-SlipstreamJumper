using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Howley
{
    public class SpringBlock : OverlapObject
    {
        public override void OnOverlap(PlayerMovement pm)
        {
            pm.LauchPlayer(new Vector3(0, 25, 0));
        }
    }
}

