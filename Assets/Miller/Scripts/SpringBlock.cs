using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Miller
{
    // spawns a spring block that launches you into the air
    public class SpringBlock : OverlapObject
    {

        public override void OnOverlap(PlayerMovement pm)
        {
            pm.LaunchPlayer(new Vector3(0, 20, 0));
            
        }
    }
}
