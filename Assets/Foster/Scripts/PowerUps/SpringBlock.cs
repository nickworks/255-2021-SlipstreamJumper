using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Foster
{
    public class SpringBlock : OverlapObjects
    {

        public override void OnOverlap(PlayerMovement pm)
        {
            
            pm.LaunchPlayer(new Vector3(0, 30, 0));
            
        }
    }
}