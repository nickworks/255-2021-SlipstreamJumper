using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Velting
{
  
    public class HazardBlock : OverlapObject
    {

        public float damageAmount = 25;
        public override void OnOverlap(PlayerMovement pm)
        {
            //player overlaps
            HealthSystem health = pm.GetComponent<HealthSystem>();

            if (health)
            {
                health.TakeDamage(damageAmount);//player takes damage
            }

            //pushes back player after taking damage
            Vector3 vToPlayer = (pm.transform.position - this.transform.position).normalized;

            pm.LaunchPlayer(vToPlayer * 10);
        }
    }
}