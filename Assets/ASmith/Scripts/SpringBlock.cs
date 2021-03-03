using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASmith
{
    public class SpringBlock : OverlapObject
    {
        /// <summary>
        /// Springblock cooldown to prevent audio stacking
        /// </summary>
        private float cooldownSpringBlock = 0;

        private void Update()
        {
            if (cooldownSpringBlock > 0)
            {
                cooldownSpringBlock -= Time.deltaTime; // if cooldownSpringBlock still has time life, countdown timer
            }
        }

        public override void OnOverlap(PlayerMovement pm)
        {
            pm.LaunchPlayer(new Vector3(0, 20, 0)); // Launch player upward on overlap
            if (cooldownSpringBlock > 0) return; // still on cooldown, dont play audio
            cooldownSpringBlock = .25f; // cooldown till audio can play again
            SoundEffectBoard.PlaySpringBlock(); // plays springblock audio
        }
    }
}

