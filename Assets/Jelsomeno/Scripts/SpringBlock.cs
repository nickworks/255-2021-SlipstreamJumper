using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jelsomeno
{
    public class SpringBlock : OverlapObject
    {
        /// <summary>
        /// made this public float so each spring block can have a different launch power if I want it to
        /// </summary>
        public float launch = 25;

        public void PlayerHit(PlayerMovement pm)
        {
            pm.LaunchPlayer(new Vector3(0, launch, 0));
        }

    }
}
