using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hodgkins
{
    public class SpringBlock : OverlapObject
    {
        /// <summary>
        /// What happens when the player touches the Spring
        /// </summary>
        /// <param name="pm"></param>
        public override void OnOverlap(PlayerMovement pm)
        {
            pm.LaunchPlayer(new Vector3(0, 20, 0));
            SoundEffectBoard.PlaySpring();
        }
    }
}