using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASmith
{
    public class HazardBlock : OverlapObject
    {
        /// <summary>
        /// How much damage to take on overlap
        /// </summary>
        public float damageAmount = 25;

        public override void OnOverlap(PlayerMovement pm)
        {
            HealthSystem health = pm.GetComponent<HealthSystem>(); // Gets a reference to the HealthSystem class for access to the health variable

            if (health) // if player still has health...
            {
                health.TakeDamage(damageAmount); // ...take damage
            }

            Vector3 vToPlayer = (pm.transform.position - this.transform.position).normalized; 

            pm.LaunchPlayer(vToPlayer * 15); // slight knockback on overlap
        }
    }
}

