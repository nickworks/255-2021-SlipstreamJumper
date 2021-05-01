using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Miller
{
    // spawns a hazard block that does damage to you
    public class HazardBlock : OverlapObject
    {

        public float damageAmount = 25;

        public override void OnOverlap(PlayerMovement pm)
        {

            HealthSystem health = pm.GetComponent<HealthSystem>();

            if(health)
            {
                health.TakeDamage(damageAmount);
            }

            // add knockback

            Vector3 vToPlayer = (pm.transform.position - this.transform.position).normalized;
            

            pm.LaunchPlayer(vToPlayer * 10);

        }
    }
}
