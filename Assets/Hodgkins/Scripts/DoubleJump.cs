using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hodgkins
{
    public class DoubleJump : OverlapObject
    {
        /// <summary>
        /// What happens when the player touches the Powerup
        /// </summary>
        /// <param name="pm"></param>
        public override void OnOverlap (PlayerMovement pm)
        {
            Destroy(gameObject);
            pm.canDoubleJump = true;
            SoundEffectBoard.PlayPowerup();
        }
    }
}