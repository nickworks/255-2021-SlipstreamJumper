using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak {

    /// <summary>
    /// This Class is for when the player overlaps with a powerup object
    /// </summary>
    public class PowerUp : OverlapObject {

        /// <summary>
        /// Runs when the player overlaps with a power up object
        /// </summary>
        /// <param name="pm"></param>
        public override void OnOverlap(PlayerMovement pm) {
            PlayerMovement doubleJump = pm.GetComponent<PlayerMovement>();

            // If the local variable has the PlayerMovement 
            if (doubleJump) {
                doubleJump.powerUpSpeed += 2; // adds two speed to player 
                SoundEffectBoard.PowerUpSound();
                Destroy(gameObject); // destroy power up game object
            }
        }
    }
}