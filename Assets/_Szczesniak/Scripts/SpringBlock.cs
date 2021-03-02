using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak {
    /// <summary>
    /// This Class's purpose is for when the player overlaps with the spring block
    /// </summary>
    public class SpringBlock : OverlapObject {
       
        // When the player overlaps a Spring Block in game
        public void PlayerHit(PlayerMovement pm) {
        }

        /// <summary>
        /// Adds force to the player when they overlap it
        /// </summary>
        /// <param name="pm"></param>
        public override void OnOverlap(PlayerMovement pm) {
            SoundEffectBoard.BoastSound();
            pm.LaunchPlayer(new Vector3(50, 25, 0)); // This adds force to the player and launches them up and forward
        }
        
    }
}