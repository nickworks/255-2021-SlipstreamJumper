using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Howley
{
    public class Hazard : OverlapObject
    {
        /// <summary>
        /// This is the amount of damage the player will take from hitting a hazard.
        /// </summary>
        public float damageAmount = 25;

        /// <summary>
        /// This function overrides the OnOverlap within the OverlapObject class.
        /// </summary>
        /// <param name="pm"></param>
        public override void OnOverlap(PlayerMovement pm)
        {
            HealthSystem health = pm.GetComponent<HealthSystem>();

            if (health)
            {
                health.TakeDamage(damageAmount);
            }

            Vector3 vToPlayer = (pm.transform.position - this.transform.position).normalized;
            
            pm.LauchPlayer(vToPlayer * 15);
        }
    }
}

