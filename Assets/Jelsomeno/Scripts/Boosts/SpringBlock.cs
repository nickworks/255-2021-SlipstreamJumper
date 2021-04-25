using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jelsomeno
{
    /// <summary>
    /// class system for the spring block object/obstacle
    /// </summary>
    public class SpringBlock : OverlapObject
    {
        /// <summary>
        /// this public float so each spring block can have a different launch power if I want it to
        /// </summary>
        public float launch = 25;

        /// <summary>
        /// adds a launch effect to player when overlap or hit it
        /// </summary>
        /// <param name="pm"></param>
        public void PlayerHit(PlayerMovement pm)
        {
            pm.LaunchPlayer(new Vector3(0, launch, 0));// this launches the player up on y axis
        }

    }
}
