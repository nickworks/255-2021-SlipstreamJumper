using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Howley
{
    public class SpringBlock : OverlapObject
    {
        /// <summary>
        /// This function launches the player when they overlap a spring block.
        /// </summary>
        /// <param name="pm"></param>
        public override void OnOverlap(PlayerMovement pm)
        {
            pm.LauchPlayer(new Vector3(0, 20, 0));
        }
    }
}

