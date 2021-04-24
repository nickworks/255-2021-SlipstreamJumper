using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelsomeno
{
    /// <summary>
    /// this is a class for a simple hazard obstacle
    /// </summary>
    public class Hazard : OverlapObject
    {
        // how much damage the hazard can do
        public float damageAmount = 25;

        /// <summary>
        /// once the player overlaps with the hazard
        /// </summary>
        /// <param name="pm"></param>
        public override void OnOverlap(PlayerMovement pm)
        {
            // hp 
            Health hp = pm.GetComponent<Health>();// is getting a reference to the player healrh

            if (hp)
            {
                hp.TakeDamage(damageAmount);// do damage to the player
            }

            Vector3 vToPlayer = (pm.transform.position - this.transform.position).normalized.normalized;

            pm.LaunchPlayer(vToPlayer * 7);// when the player its the hazard it will knock it back


        }

    }
}
