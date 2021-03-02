using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak {
    /// <summary>
    /// This Class's purpose is for when the player overlaps with the Hazard block
    /// </summary>
    public class HazardBlock : OverlapObject {

        /// <summary>
        /// The amount of damage the player will take 
        /// </summary>
        public float damageAmount = 25;

        /// <summary>
        /// If the player overlaps the hazard block and takes the player's health away
        /// </summary>
        /// <param name="pm"></param>
        public override void OnOverlap(PlayerMovement pm) {

            HealthSystem health = pm.GetComponent<HealthSystem>();

            // If the health is stored
            if (health) {
                health.TakeDamage(damageAmount);
            }

            // add knock-back to the player

            Vector3 vToPlayer = (pm.transform.position - this.transform.position).normalized;


            pm.LaunchPlayer(vToPlayer * 15);

            // player overlaps!

        }
    }
}